using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.SarciniRealizate
{
    public class EditModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public EditModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SarcinaRealizata SarcinaRealizata { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SarcinaRealizata == null)
            {
                return NotFound();
            }
            ListaProvocari = await _context.Provocare.ToListAsync();


            var sarcinarealizata =  await _context.SarcinaRealizata.FirstOrDefaultAsync(m => m.Id == id);
            if (sarcinarealizata == null)
            {
                return NotFound();
            }
            SarcinaRealizata = sarcinarealizata;
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

            _context.Attach(SarcinaRealizata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SarcinaRealizataExists(SarcinaRealizata.Id))
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

        private bool SarcinaRealizataExists(int id)
        {
          return (_context.SarcinaRealizata?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
