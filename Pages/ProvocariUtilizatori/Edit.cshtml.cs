using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.ProvocariUtilizatori
{
    public class EditModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public EditModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProvocareUtilizator ProvocareUtilizator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProvocareUtilizator == null)
            {
                return NotFound();
            }

            var provocareutilizator =  await _context.ProvocareUtilizator.FirstOrDefaultAsync(m => m.Id == id);
            if (provocareutilizator == null)
            {
                return NotFound();
            }
            ProvocareUtilizator = provocareutilizator;
           ViewData["ProvocareId"] = new SelectList(_context.Provocare, "Id", "Id");
           ViewData["UtilizatorId"] = new SelectList(_context.Utilizator, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProvocareUtilizator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvocareUtilizatorExists(ProvocareUtilizator.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProvocareUtilizatorExists(int id)
        {
          return (_context.ProvocareUtilizator?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
