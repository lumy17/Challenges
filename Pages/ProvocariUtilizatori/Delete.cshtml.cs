using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.WebApp.Pages.ProvocariUtilizatori
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ProvocareUtilizator ProvocareUtilizator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProvocareUtilizator == null)
            {
                return NotFound();
            }

            var provocareutilizator = await _context.ProvocareUtilizator.FirstOrDefaultAsync(m => m.Id == id);

            if (provocareutilizator == null)
            {
                return NotFound();
            }
            else 
            {
                ProvocareUtilizator = provocareutilizator;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ProvocareUtilizator == null)
            {
                return NotFound();
            }
            var provocareutilizator = await _context.ProvocareUtilizator.FindAsync(id);

            if (provocareutilizator != null)
            {
                ProvocareUtilizator = provocareutilizator;
                _context.ProvocareUtilizator.Remove(ProvocareUtilizator);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
