using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.CategoriiProvocari
{
    public class DeleteModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CategorieProvocare CategorieProvocare { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CategorieProvocare == null)
            {
                return NotFound();
            }

            var categorieprovocare = await _context.CategorieProvocare.FirstOrDefaultAsync(m => m.Id == id);
            ListaProvocari = await _context.Provocare.ToListAsync();

            if (categorieprovocare == null)
            {
                return NotFound();
            }
            else 
            {
                CategorieProvocare = categorieprovocare;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CategorieProvocare == null)
            {
                return NotFound();
            }
            var categorieprovocare = await _context.CategorieProvocare.FindAsync(id);

            if (categorieprovocare != null)
            {
                CategorieProvocare = categorieprovocare;
                _context.CategorieProvocare.Remove(CategorieProvocare);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
