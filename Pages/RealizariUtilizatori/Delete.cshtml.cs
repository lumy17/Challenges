using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.RealizariUtilizatori
{
    public class DeleteModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public RealizareUtilizator RealizareUtilizator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RealizareUtilizator == null)
            {
                return NotFound();
            }

            var realizareutilizator = await _context.RealizareUtilizator.FirstOrDefaultAsync(m => m.Id == id);

            if (realizareutilizator == null)
            {
                return NotFound();
            }
            else 
            {
                RealizareUtilizator = realizareutilizator;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.RealizareUtilizator == null)
            {
                return NotFound();
            }
            var realizareutilizator = await _context.RealizareUtilizator.FindAsync(id);

            if (realizareutilizator != null)
            {
                RealizareUtilizator = realizareutilizator;
                _context.RealizareUtilizator.Remove(RealizareUtilizator);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
