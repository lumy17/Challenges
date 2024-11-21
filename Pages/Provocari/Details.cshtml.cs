using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.Provocari
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Challenge Challenge { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Challenge == null)
            {
                return NotFound();
            }
            var provocare = await _context.Challenge
                .FirstOrDefaultAsync(m => m.Id == id);
            Challenges = await _context.Challenge.ToListAsync();
            if (provocare == null)
            {
                return NotFound();
            }
            else
            {
                Challenge = provocare;
            }
            var currentUser = User.Identity.Name;
            var user = _context.AppUser.FirstOrDefault(u => u.Email == currentUser);
            //de fiecare data cand un vizitator da click pe o provocare
            //vizulizarile vor creste
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
            return RedirectToPage("/Account/Login", new { area = "Identity" }); // Redirect to login page

        }
    }
}