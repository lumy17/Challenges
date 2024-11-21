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
        public string CurrentUser { get; set; }
        public List<Badge> Badges { get; set; }
        public List<TodoTask> CurrentTasks { get; set; } = new List<TodoTask>();
        public List<Challenge> Challenges { get; set; }

        public dashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Challenges = _context.Challenge.ToList();
            CurrentUser = User.Identity.Name;
            var user = _context.AppUser.FirstOrDefault
                (u => u.Email == CurrentUser);
            if (user != null)
            {
                FinishedChallengesCount = _context.UserChallenge
                .Count(p => p.AppUserId == user.Id
                && p.CurrentState == "finished");

                Streak = user.Streak;
                Points = user.Points;

                Badges = _context.Badge.ToList();
                var userChallenge = _context.UserChallenge.FirstOrDefault
                    (u => u.AppUserId == user.Id);
                //verifica daca utilizatorul curent a realizat o sarcina adica
                //daca exista o data_realizare cu data de azi pentru
                //o provocare specifica a unui utilizator
                if (userChallenge != null)
                {
                    bool taskCompletedToday = _context.FinishedTask
                        .Any(u => u.UserChallengeId == userChallenge.Id
                        && u.CompletionDate.Value.Date == DateTime.Today);
                    // Verifica daca utilizatorul a finalizat o sarcina ieri
                    bool taskCompletedYesterday = _context.FinishedTask
                        .Any(u => u.UserChallengeId == userChallenge.Id
                        && u.CompletionDate.Value.Date == DateTime.Today.AddDays(-1));

                    if (taskCompletedToday && user.LastStreakUpdateDate != DateTime.Today)
                    {
                        user.Streak++;
                        user.LastStreakUpdateDate = DateTime.Today;

                        _context.Update(user);
                        _context.SaveChanges();
                    }
                    // Daca utilizatorul nu a finalizat o sarcina ieri, reseteaza streak-ul la 0
                    else if (!taskCompletedYesterday && !taskCompletedToday)
                    {
                        user.Streak = 0;

                        _context.Update(user);
                        _context.SaveChanges();
                    }
                }
                Streak = user.Streak;

                var userChallenges = _context.UserChallenge
    .Where(pu => pu.AppUserId == user.Id && pu.CurrentState != "finished")
    .ToList();
                if (userChallenges.Count != 0)
                {
                    bool taskCompletedToday = _context.FinishedTask
    .Any(u => u.UserChallengeId == userChallenge.Id
    && u.CompletionDate.Value.Date == DateTime.Today);
                    foreach (var prov in userChallenges)
                    {
                        if (taskCompletedToday)
                        {
                            var currentTask = _context.TodoTask.FirstOrDefault(s =>
    s.ChallengeId == prov.ChallengeId
    && s.Day == prov.CurrentDay-1);
                            if (currentTask != null)
                            {
                                CurrentTasks.Add(currentTask);
                            }
                        }
                        else {
                            var currentTask = _context.TodoTask.FirstOrDefault(s =>
        s.ChallengeId == prov.ChallengeId
        && s.Day == prov.CurrentDay);
                            if (currentTask != null)
                            {
                                CurrentTasks.Add(currentTask);
                            }
                        }
                    }
                }
            }
        }
        public bool UserHasBadge(int badgeId)
        {
            var user = _context.AppUser.FirstOrDefault(u => u.Email == CurrentUser);
            return _context.UserBadge.Any(ru => ru.UserId == user.Id && ru.BadgeId == badgeId);
        }
    }
}
