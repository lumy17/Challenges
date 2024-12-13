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

namespace Challenges.WebApp.Pages.Badges
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Challenges = _context.Challenge.ToList();

            return Page();
        }

        [BindProperty]
        public Badge Badge { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Badge == null || Badge == null)
            {
                return Page();
            }

            _context.Badge.Add(Badge);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
