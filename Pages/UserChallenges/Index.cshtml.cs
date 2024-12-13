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
    public class IndexModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserChallenge> UserChallenges { get;set; } = default!;
        public List<Challenge> Challenges { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.UserChallenge != null)
            {
                UserChallenges = await _context.UserChallenge
                .Include(p => p.Challenge)
                .Include(p => p.AppUser).ToListAsync();
            }
            Challenges = await _context.Challenge.ToListAsync();

        }
    }
}
