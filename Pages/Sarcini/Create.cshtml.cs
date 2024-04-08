using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Sarcini
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProvocareId"] = new SelectList(_context.Provocare, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Sarcina Sarcina { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Sarcina == null || Sarcina == null)
            {
                return Page();
            }

            _context.Sarcina.Add(Sarcina);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
