using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.FinishedTasks
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<FinishedTask> FinishedTask { get; set; } = default!;

        public async Task OnGetAsync(int userChallengeId)
        {
            FinishedTask = await _context.FinishedTask
                    .Where(sr => sr.UserChallengeId == userChallengeId)
                    .Include(s => s.TodoTask)
                    .Include(sr => sr.UserChallenge)
                        .ThenInclude(pu => pu.Challenge)
                    .Include(sr => sr.UserChallenge)
                        .ThenInclude(pu => pu.AppUser)
                    .ToListAsync();
        }
    }
}
