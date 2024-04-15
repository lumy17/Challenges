using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Provocari
{
    public class DetailsAdminModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DetailsAdminModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
