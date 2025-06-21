using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.UserChallenges
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserChallenge> UserChallenges { get; set; }
        
        public List<Challenge> Challenges { get; set; }

        public async Task OnGetAsync()
        {
            Challenges = await _context.Challenge.ToListAsync();
            var currentUser = User.Identity.Name;
            var user = _context.AppUser.FirstOrDefault(u => u.Email == currentUser);

            if (user != null)
            {
                UserChallenges = await _context.UserChallenge
                    .Include(p => p.Challenge)
                    .Where(u => u.AppUserId == user.Id)
                    .ToListAsync();
            }

        }
    }
}
