using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.WebApp.Pages.UserChallenges
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UserChallenge UserChallenge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserChallenge == null)
            {
                return NotFound();
            }

            var userChallenge = await _context.UserChallenge.FirstOrDefaultAsync(m => m.Id == id);

            if (userChallenge == null)
            {
                return NotFound();
            }
            else 
            {
                UserChallenge = userChallenge;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserChallenge == null)
            {
                return NotFound();
            }
            var userChallenge = await _context.UserChallenge.FindAsync(id);

            if (userChallenge != null)
            {
                UserChallenge = userChallenge;
                _context.UserChallenge.Remove(UserChallenge);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
