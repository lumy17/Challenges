using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Challenges.WebApp.Pages.AppChallenges
{
    public class TasksModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TasksModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Challenge Challenge { get; set; } = default!;

        public List<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();

        public List<Challenge> Challenges { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int Count { get; set; }

        public int PageSize { get; set; } = 1;

        public bool ShowPrevious => CurrentPage > 1;

        public bool ShowNext => CurrentPage < Count;

        public List<DateTime> FinishedTasks { get; set; } = new List<DateTime>();

        public async Task<IActionResult> OnGetAsync(int? id, int? currentpage)
        {
            if (id == null || _context.Challenge == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenge.FirstOrDefaultAsync(m => m.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }
            else
            {
                Challenge = challenge;
                var allTasks = await _context.TodoTask.Where(s => s.ChallengeId == id).ToListAsync();
                CurrentPage = currentpage ?? 1;

                TodoTasks = allTasks.OrderBy(d => d.Day).Skip(CurrentPage - 1).Take(PageSize).ToList();
            }

            Challenges = await _context.Challenge.ToListAsync();
            Count = Challenge.Duration;

            return Page();
        }
        public async Task<IActionResult> OnPostMarkTaskAsCompletedAsync(int idTask, int idChallenge)
        {
            var currentUser = User.Identity.Name;
            var user = _context.AppUser.FirstOrDefault(u => u.Email == currentUser);

            Challenge = await _context.Challenge.FirstOrDefaultAsync(m => m.Id == idChallenge);

            var userChallenge = _context.UserChallenge.FirstOrDefault
                (pu => pu.AppUserId == user.Id && pu.ChallengeId == Challenge.Id);

            var todoTask = _context.TodoTask.FirstOrDefault(s => s.Id == idTask);

            var finishedTask = new FinishedTask
            {
                UserChallengeId = userChallenge.Id,
                TodoTaskId = todoTask.Id,
                CompletionDate = DateTime.Now,
                CompletionDay = todoTask.Day
            };

            userChallenge.CurrentDay++;
            user.Points++;

            _context.FinishedTask.Add(finishedTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Dashboards/dashboard");
        }
        public async Task<IActionResult> OnPostMarkChallengeAsCompletedAsync(int id)
        {
            var currentUser = User.Identity.Name;
            var user = _context.AppUser.FirstOrDefault(u => u.Email == currentUser);

            var userChallenge = _context.UserChallenge.FirstOrDefault
                (pu => pu.AppUserId == user.Id && pu.ChallengeId == id);

            userChallenge.CurrentState = "Completed";
            userChallenge.EndDate = DateTime.Now;

            var todoTask = _context.TodoTask.FirstOrDefault(s => s.Id == id);

            var finishedTask = new FinishedTask
            {
                UserChallengeId = userChallenge.Id,
                TodoTaskId = todoTask.Id,
                CompletionDate = DateTime.Now,
                CompletionDay = todoTask.Day
            };

            userChallenge.CurrentDay++;
            user.Points++;

            _context.FinishedTask.Add(finishedTask);
            _context.Update(userChallenge);

            await _context.SaveChangesAsync();

            await AwardNewBadge(user);

            return RedirectToPage("../dashboard");
        }

        public async Task AwardNewBadge(AppUser user)
        {
            var nextBadge = _context.Badge.FirstOrDefault
                (b => !_context.UserBadge.Any(ru => ru.UserId == user.Id && ru.BadgeId == b.Id));

            if (nextBadge != null)
            {
                var userBadge = new UserBadge
                {
                    UserId = user.Id,
                    BadgeId = nextBadge.Id,
                    Date = DateTime.Now
                };
                _context.UserBadge.Add(userBadge);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsTaskAvailable(int taskId)
        {
            var currentUser = User.Identity.Name;
            var user = _context.AppUser.FirstOrDefault(u => u.Email == currentUser);
            var userChallenge = _context.UserChallenge.FirstOrDefault(pu => pu.AppUserId == user.Id && pu.ChallengeId == Challenge.Id);

            var todoTask = _context.TodoTask.FirstOrDefault(s => s.Id == taskId);
            var currentDay = DateTime.Today.Day;

            var firstTask = await _context.TodoTask.FirstOrDefaultAsync(s => s.ChallengeId == Challenge.Id
            && s.Day == 1);


			if (firstTask.Id == taskId)
            {
                return true;
            }

            else
            {
                return _context.FinishedTask.Any(
                    sr => sr.CompletionDay+1 == todoTask.Day &&
                    sr.UserChallengeId == userChallenge.Id
                    && sr.CompletionDate < DateTime.Today);
            }
        }

    }
}