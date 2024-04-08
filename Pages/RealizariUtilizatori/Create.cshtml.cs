using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.RealizariUtilizatori
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
        ViewData["RealizareId"] = new SelectList(_context.Realizare, "Id", "Id");
        ViewData["UtilizatorId"] = new SelectList(_context.Utilizator, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public RealizareUtilizator RealizareUtilizator { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.RealizareUtilizator == null || RealizareUtilizator == null)
            {
                return Page();
            }

            _context.RealizareUtilizator.Add(RealizareUtilizator);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
