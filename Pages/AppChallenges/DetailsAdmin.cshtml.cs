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

namespace Challenges.WebApp.Pages.AppChallenges
{
    [Authorize(Roles = "Admin")]
    public class DetailsAdminModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DetailsAdminModel(Challenges.WebApp.Data.ApplicationDbContext context)
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

            var challenge = await _context.Challenge.FirstOrDefaultAsync(m => m.Id == id);
            if (challenge == null)
            {
                return NotFound();
            }
            else
            {
                Challenge = challenge;
            }
            return Page();
        }
    }
}
