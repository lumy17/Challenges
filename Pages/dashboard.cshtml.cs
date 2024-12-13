using Challenges.WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using Challenges.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenges.WebApp.Pages
{
    public class dashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public int FinishedChallengesCount { get; set; }
        public int Streak { get; set; }
        public int Points { get; set; }
        public AppUser CurrentUser { get; set; }
        public string UserEmail { get; set; }
        public List<Badge> Badges { get; set; } = new List<Badge>();
        public List<TodoTask> CurrentTasks { get; set; } = new List<TodoTask>();
        public List<Challenge> Challenges { get; set; } = new List<Challenge>();

        public dashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            await PopulateDashboardAsync();
        }
        private async Task PopulateDashboardAsync()
        {
            UserEmail = User.Identity.Name;
            CurrentUser = await _context.AppUser.FirstOrDefaultAsync(u => u.Email == UserEmail);
            if (CurrentUser == null) return;
            Challenges = await _context.Challenge.ToListAsync();
            Badges = await _context.Badge.ToListAsync();
            FinishedChallengesCount = await _context.UserChallenge
                .CountAsync(uc => uc.AppUserId == CurrentUser.Id && uc.CurrentState == "finished");
            Streak = CurrentUser.Streak;
            Points = CurrentUser.Points;

            await UpdateStreakAsync();
            await PopulateCurrentTasksAsync();
        }
        private async Task UpdateStreakAsync()
        {
            var userChallenge = await _context.UserChallenge
                .FirstOrDefaultAsync(u => u.AppUserId == CurrentUser.Id);
            bool taskCompletedToday = await _context.FinishedTask
                .AnyAsync(u => u.UserChallengeId == userChallenge.Id
                && u.CompletionDate.Value.Date == DateTime.Today);
            bool taskCompletedYesterday = _context.FinishedTask
                .Any(u => u.UserChallengeId == userChallenge.Id
                && u.CompletionDate.Value.Date == DateTime.Today.AddDays(-1));
            if (taskCompletedToday && CurrentUser.LastStreakUpdateDate != DateTime.Today)
            {
                CurrentUser.Streak++;
                CurrentUser.LastStreakUpdateDate = DateTime.Today;
            }
            // Daca utilizatorul nu a finalizat o sarcina ieri, reseteaza streak-ul la 0
            else if (!taskCompletedYesterday && !taskCompletedToday)
            {
                CurrentUser.Streak = 0;
            }
            _context.Update(CurrentUser);
            await _context.SaveChangesAsync();
        }

        private async Task PopulateCurrentTasksAsync()
        {
            var userChallenges = await _context.UserChallenge
                .Where(uc => uc.AppUserId == CurrentUser.Id && uc.CurrentState != "finished")
                .ToListAsync();
            foreach (var challenge in userChallenges)
            {
                var currentTask = await GetCurrentTaskAsync(challenge);
                if (currentTask != null)
                {
                    CurrentTasks.Add(currentTask);
                }
            }
        }

        private async Task<TodoTask> GetCurrentTaskAsync(UserChallenge userChallenge)
        {
            bool taskCompletedToday = await _context.FinishedTask.AnyAsync
                (uc => uc.UserChallengeId == userChallenge.Id 
                && uc.CompletionDate.Value.Date == DateTime.Today);
            int dayOffset = taskCompletedToday ? -1 : 0;
            return await _context.TodoTask.FirstOrDefaultAsync
                (t => t.ChallengeId == userChallenge.ChallengeId
                && t.Day == userChallenge.CurrentDay + dayOffset);
        }
        public bool UserHasBadge(int badgeId)
        {
            var user = _context.AppUser.FirstOrDefault(u => u.Email == UserEmail);
            return _context.UserBadge.Any(ru => ru.UserId == user.Id && ru.BadgeId == badgeId);
        }
    }
}
