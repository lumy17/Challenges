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

namespace Challenges.WebApp.Pages.Categorii
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Category Category { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }

        public IActionResult OnGet()
        {
            Challenges = _context.Challenge.ToList();

            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Category == null || Category == null)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
