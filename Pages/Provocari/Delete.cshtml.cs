using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.Pages.Provocari
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Provocare Provocare { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Provocare == null)
            {
                return NotFound();
            }

            var provocare = await _context.Provocare.FirstOrDefaultAsync(m => m.Id == id);

            if (provocare == null)
            {
                return NotFound();
            }
            else 
            {
                Provocare = provocare;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Provocare == null)
            {
                return NotFound();
            }
            var provocare = await _context.Provocare.FindAsync(id);

            if (provocare != null)
            {
                Provocare = provocare;
                _context.Provocare.Remove(Provocare);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
