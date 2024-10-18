using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenges.WebApp.Pages.Sarcini
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Provocare> ListaProvocari { get; set; }
        public IActionResult OnGet()
        {
        ViewData["ProvocareId"] = new SelectList(_context.Provocare, "Id", "Id");
            ListaProvocari = _context.Provocare.ToList();

            return Page();
        }

        [BindProperty]
        public Sarcina Sarcina { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Sarcina == null || Sarcina == null)
            {
                return Page();
            }

            _context.Sarcina.Add(Sarcina);
            await _context.SaveChangesAsync();

			return RedirectToPage("./Index", new { id = Sarcina.ProvocareId });
		}
    }
}
