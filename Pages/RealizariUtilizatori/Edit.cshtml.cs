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

namespace Challenges.Pages.RealizariUtilizatori
{
    public class EditModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public EditModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RealizareUtilizator RealizareUtilizator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RealizareUtilizator == null)
            {
                return NotFound();
            }

            var realizareutilizator =  await _context.RealizareUtilizator.FirstOrDefaultAsync(m => m.Id == id);
            if (realizareutilizator == null)
            {
                return NotFound();
            }
            RealizareUtilizator = realizareutilizator;
           ViewData["RealizareId"] = new SelectList(_context.Realizare, "Id", "Id");
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

            _context.Attach(RealizareUtilizator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealizareUtilizatorExists(RealizareUtilizator.Id))
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

        private bool RealizareUtilizatorExists(int id)
        {
          return (_context.RealizareUtilizator?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
