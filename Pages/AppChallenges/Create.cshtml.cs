﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.WebApp.Pages.AppChallenges
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Challenge Challenge { get; set; } = default!;

        public List<Category> Categories { get; set; }

        [BindProperty]
        public List<int> SelectedCategories { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync()
        {
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

            return RedirectToPage("./IndexAdmin");
        }
    }
}
