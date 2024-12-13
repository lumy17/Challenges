using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.WebApp.Pages.AppChallenges
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Challenge Challenge { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }

        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<int> SelectedCategories { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync()
        {
            Challenges = await _context.Challenge.ToListAsync();
            Categories = await _context.Category.ToListAsync();   

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
          foreach(var categoryId in SelectedCategories)
            {
                var category = await _context.Category.FindAsync(categoryId);
                if (category != null)
                {
                    var challengeCategory = new ChallengeCategory
                    {
                        Challenge = Challenge,
                        Category = category
                    };
                    _context.ChallengeCategory.Add(challengeCategory);
                }
            }

            _context.Challenge.Add(Challenge);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
