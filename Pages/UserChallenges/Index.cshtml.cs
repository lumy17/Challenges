using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Challenges.WebApp.Data;

namespace Challenges.WebApp.Pages.UserChallenges
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserChallenge> UserChallenges { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserChallenge != null)
            {
                UserChallenges = await _context.UserChallenge
                .Include(p => p.Challenge)
                .Include(p => p.AppUser).ToListAsync();
            }
        }
    }
}
