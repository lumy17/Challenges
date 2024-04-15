using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.CategoriiProvocari
{
    public class EditModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public EditModel(Challenges.Data.ApplicationDbContext context)
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

            var categorieprovocare =  await _context.CategorieProvocare.FirstOrDefaultAsync(m => m.Id == id);
            ListaProvocari = await _context.Provocare.ToListAsync();

            if (categorieprovocare == null)
            {
                return NotFound();
            }
            CategorieProvocare = categorieprovocare;
           ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id");
           ViewData["ProvocareId"] = new SelectList(_context.Provocare, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CategorieProvocare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieProvocareExists(CategorieProvocare.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategorieProvocareExists(int id)
        {
          return (_context.CategorieProvocare?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
