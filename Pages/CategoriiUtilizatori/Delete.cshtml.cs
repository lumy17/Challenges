using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.CategoriiUtilizatori
{
    public class DeleteModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CategorieUtilizator CategorieUtilizator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CategorieUtilizator   == null)
            {
                return NotFound();
            }

            var categorieutilizator = await _context.CategorieUtilizator.FirstOrDefaultAsync(m => m.Id == id);

            if (categorieutilizator == null)
            {
                return NotFound();
            }
            else 
            {
                CategorieUtilizator = categorieutilizator;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CategorieUtilizator == null)
            {
                return NotFound();
            }
            var categorieutilizator = await _context.CategorieUtilizator.FindAsync(id);

            if (categorieutilizator != null)
            {
                CategorieUtilizator = categorieutilizator;
                _context.CategorieUtilizator.Remove(CategorieUtilizator);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
