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

namespace Challenges.Pages.Sarcini
{
    public class EditModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public EditModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sarcina Sarcina { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sarcina == null)
            {
                return NotFound();
            }

            var sarcina =  await _context.Sarcina.FirstOrDefaultAsync(m => m.Id == id);
            if (sarcina == null)
            {
                return NotFound();
            }
            Sarcina = sarcina;
                 ListaProvocari = await _context.Provocare.ToListAsync();
            ViewData["ProvocareId"] = new SelectList(_context.Provocare, "Id", "Id");
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

            _context.Attach(Sarcina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SarcinaExists(Sarcina.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

			return RedirectToPage("./Index", new { id = Sarcina.ProvocareId });
		}

		private bool SarcinaExists(int id)
        {
          return (_context.Sarcina?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
