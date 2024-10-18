using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.Realizari
{
    public class DeleteModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Realizare Realizare { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Realizare == null)
            {
                return NotFound();
            }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Realizare == null)
            {
                return NotFound();
            }
            var realizare = await _context.Realizare.FindAsync(id);

            if (realizare != null)
            {
                Realizare = realizare;
                _context.Realizare.Remove(Realizare);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
