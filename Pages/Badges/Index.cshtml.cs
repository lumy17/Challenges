using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.Badges
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Badge> Badges { get;set; } = default!;
        public List<Challenge> Challenges { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Badge != null)
            {
                Badges = await _context.Badge.ToListAsync();
            }
            Challenges = await _context.Challenge.ToListAsync();

        }
    }
}
