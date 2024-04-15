using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Realizari
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Realizare Realizare { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Realizare == null)
            {
                return NotFound();
            }
            ListaProvocari = await _context.Provocare.ToListAsync();

            var realizare = await _context.Realizare.FirstOrDefaultAsync(m => m.Id == id);
            if (realizare == null)
            {
                return NotFound();
            }
            else 
            {
                Realizare = realizare;
            }
            return Page();
        }
    }
}
