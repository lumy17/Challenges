using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Utilizatori
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Utilizator Utilizator { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Utilizator == null)
            {
                return NotFound();
            }

            var utilizator = await _context.Utilizator.FirstOrDefaultAsync(m => m.Id == id);
            ListaProvocari = await _context.Provocare.ToListAsync();

            if (utilizator == null)
            {
                return NotFound();
            }
            else 
            {
                Utilizator = utilizator;
            }
            return Page();
        }
    }
}
