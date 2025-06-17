using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.AppChallenges
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Challenge> Challenges { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Challenge != null)
            {
                Challenges = await _context.Challenge
                    .Include(c => c.ChallengeCategories)
                        .ThenInclude(cc => cc.Category)
                    .ToListAsync();
            }
        }
    }
}
