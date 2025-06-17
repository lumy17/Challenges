using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.WebApp.Pages.AppChallenges
{
    [Authorize(Roles = "Admin")]
    public class IndexAdminModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexAdminModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Challenge> Challenges { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Challenge != null)
            {
                Challenges = await _context.Challenge
                    .Include(cp => cp.ChallengeCategories)
                    .ThenInclude(c=>c.Category)
                    .ToListAsync();
            }
        }
    }
}
