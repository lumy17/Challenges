using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.SarciniRealizate
{
    public class DeleteModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public SarcinaRealizata SarcinaRealizata { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SarcinaRealizata == null)
            {
                return NotFound();
            }

            var sarcinarealizata = await _context.SarcinaRealizata.FirstOrDefaultAsync(m => m.Id == id);

            if (sarcinarealizata == null)
            {
                return NotFound();
            }
            else 
            {
                SarcinaRealizata = sarcinarealizata;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SarcinaRealizata == null)
            {
                return NotFound();
            }
            var sarcinarealizata = await _context.SarcinaRealizata.FindAsync(id);

            if (sarcinarealizata != null)
            {
                SarcinaRealizata = sarcinarealizata;
                _context.SarcinaRealizata.Remove(SarcinaRealizata);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
