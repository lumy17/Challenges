using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using MessagePack;

namespace Challenges.WebApp.Pages.FinishedTasks
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FinishedTask> FinishedTask { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }

        public async Task OnGetAsync(int? userChallengeId)
        {
            if (userChallengeId.HasValue)
            {
                FinishedTask = await _context.FinishedTask
                    .Where(sr => sr.UserChallengeId == userChallengeId.Value)
                    .Include(s => s.TodoTask)
                    .Include(sr => sr.UserChallenge)
                    .ThenInclude(pu => pu.Challenge)
                    .Include(sr => sr.UserChallenge)
                    .ThenInclude(pu => pu.AppUser)
                    .ToListAsync();
            }
            else
            {
                FinishedTask = await _context.FinishedTask.ToListAsync();
            }
            Challenges = await _context.Challenge.ToListAsync();

        }
    }
}
