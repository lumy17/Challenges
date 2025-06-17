using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.AppChallenges
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Challenge Challenge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Challenge == null)
            {
                return NotFound();
            }
            var challenge = await _context.Challenge
                .Include(c => c.ChallengeCategories)
                    .ThenInclude(cc => cc.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }
            else
            {
                Challenge = challenge;
            }

            Challenge.Views++;
            await _context.SaveChangesAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Challenge = await _context.Challenge.FirstOrDefaultAsync(m => m.Id == id);
            if (Challenge == null)
            {
                return NotFound();
            }

            var currentUser = User.Identity.Name;
            var user = _context.AppUser.FirstOrDefault(u => u.Email == currentUser);

            if (user != null)
            {
                var userChallenge = new UserChallenge
                {
                    ChallengeId = Challenge.Id,
                    AppUserId = user.Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(Challenge.Duration),
                    CurrentDay = 1,
                    CurrentState = "In desfasurare"
                };
                _context.UserChallenge.Add(userChallenge);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Tasks", new { id = Challenge.Id });
            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}