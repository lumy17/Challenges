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

namespace Challenges.WebApp.Pages.Provocari
{
    [Authorize(Roles = "Admin")]
    public class IndexAdminModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public IndexAdminModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Challenge> Challenges { get;set; } = default!;

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
