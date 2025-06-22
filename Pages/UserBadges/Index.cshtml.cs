using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.UserBadges
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserBadge> UserBadge { get;set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.UserBadge != null)
            {
                UserBadge = await _context.UserBadge
                .Include(r => r.Badge)
                .Include(r => r.User).ToListAsync();
            }
        }
    }
}
