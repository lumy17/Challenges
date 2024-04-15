using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.SarciniRealizate
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public SarcinaRealizata SarcinaRealizata { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SarcinaRealizata == null)
            {
                return NotFound();
            }
            ListaProvocari = await _context.Provocare.ToListAsync();


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
    }
}
