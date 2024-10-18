using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.Sarcini
{
    public class DeleteModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Sarcina Sarcina { get; set; } = default!;

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Sarcina == null)
            {
                return NotFound();
            }
            var sarcina = await _context.Sarcina.FindAsync(id);

            if (sarcina != null)
            {
                Sarcina = sarcina;
                _context.Sarcina.Remove(Sarcina);
                await _context.SaveChangesAsync();
            }

			return RedirectToPage("./Index", new { id = Sarcina.ProvocareId });
		}
	}
}
