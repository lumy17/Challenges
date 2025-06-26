using Challenges.WebApp.Data;
using Challenges.WebApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Challenges.WebApp.Pages.Dashboards
{
    [Authorize(Roles = "Admin")]
    public class DashboardAdminModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardAdminModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public int UserRegistrations { get; set; }
        public int StartedChallenges { get; set; }
        public int FinishedChallenges { get; set; }
        public List<string> Activities { get; set; } = new List<string>();
        public async Task OnGetAsync()
        {
            await PopulateAdminDashboard();
        }
        private async Task PopulateAdminDashboard()
        {
            UserRegistrations = await _context.AppUser.CountAsync();
            StartedChallenges = await _context.UserChallenge.CountAsync(uc => uc.StartDate != null);
            FinishedChallenges = await _context.UserChallenge.CountAsync(uc => uc.CurrentState == "finished");
            Activities = await GetActivities();
        }
        private async Task<List<string>> GetActivities()
        {
            var finalList = (await GetLastFinishedChallenges())
                .Concat(await GetLastFinishedTasks())
                .Concat(await GetLastStartedChallenges())
                .OrderBy(s =>
                {
                    var splitParts = s.Split("now ");
                    return splitParts.Length >= 2 ? splitParts[1].Split(" months")[0] : "";
                })
                .ToList();

            return finalList;
        }
        private async Task<List<string>> GetLastFinishedChallenges()
        {
            var lastFinishedChallenges = await _context.UserChallenge
                .Where(uc => uc.CurrentState == "finished")
                .Include(uc => uc.AppUser)
                .Include(uc => uc.Challenge)
                .OrderByDescending(uc => uc.EndDate)
                .Select(uc => new
                {
                    AppUser = uc.AppUser.FirstName,
                    Challenge = uc.Challenge.Name,
                    uc.EndDate
                })
                .ToListAsync();

            return lastFinishedChallenges.Select(uc =>
            {
                var interval = DateTime.Now - uc.EndDate.Value;
                var formattedInterval = interval.FormatTimeInterval();
                return $"User {uc.AppUser} has finished challenge \"{uc.Challenge}\" {formattedInterval}";
            }).ToList();
        }
        private async Task<List<string>> GetLastFinishedTasks()
        {
            var lastFinishedTasks = await _context.FinishedTask
                .OrderByDescending(ft => ft.CompletionDate)
                .ToListAsync();

            return lastFinishedTasks.Select(ft =>
            {
                var appUser = _context.AppUser.Find(_context.UserChallenge
                    .Find(ft.UserChallengeId).AppUserId).FirstName;
                var todoTask = _context.TodoTask.Find(ft.TodoTaskId).Name;
                var interval = DateTime.Now - ft.CompletionDate.Value;
                var formattedInterval = interval.FormatTimeInterval();
                return $"User {appUser} has finished the task \"{todoTask}\" {formattedInterval}";
            }).ToList();
        }
        private async Task<List<string>> GetLastStartedChallenges()
        {
            var lastStartedChallenges = await _context.UserChallenge
                .Where(uc => uc.StartDate != null && uc.CurrentState != "finished")
                .OrderByDescending(uc => uc.StartDate)
                .ToListAsync();

            return lastStartedChallenges.Select(uc =>
            {
                var appUser = _context.AppUser.Find(uc.AppUserId).FirstName;
                var challenge = _context.Challenge.Find(uc.ChallengeId).Name;
                var interval = DateTime.Now - uc.StartDate.Value;
                var formattedInterval = interval.FormatTimeInterval();
                return $"User {appUser} has started the challenge \"{challenge}\" {formattedInterval}";
            }).ToList();
        }
    }
}
