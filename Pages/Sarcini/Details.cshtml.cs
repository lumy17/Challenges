using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Sarcini
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Sarcina Sarcina { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sarcina == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcina.FirstOrDefaultAsync(m => m.Id == id);
            if (sarcina == null)
            {
                return NotFound();
            }
            else 
            {
                Sarcina = sarcina;
            }
            ListaProvocari = await _context.Provocare.ToListAsync();

            return Page();
        }
    }
}
