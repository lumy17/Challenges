using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Categorii
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Categorie Categorie { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categorie == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorie.FirstOrDefaultAsync(m => m.Id == id);
            ListaProvocari = await _context.Provocare.ToListAsync();

            if (categorie == null)
            {
                return NotFound();
            }
            else 
            {
                Categorie = categorie;
            }
            return Page();
        }
    }
}
