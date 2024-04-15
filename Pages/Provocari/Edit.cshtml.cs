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

namespace Challenges.Pages.Provocari
{
    public class EditModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public EditModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Provocare Provocare { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }
        public List<Categorie> ListaCategorii { get; set; }
        [BindProperty]
        public List<int> SelectedCategories { get; set; } = new List<int>();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Provocare == null)
            {
                return NotFound();
            }

            var provocare =  await _context.Provocare.FirstOrDefaultAsync(m => m.Id == id);
            if (provocare == null)
            {
                return NotFound();
            }
            Provocare = provocare;
            ListaProvocari = await _context.Provocare.ToListAsync();
            ListaCategorii = await _context.Categorie.ToListAsync();

            // Fetch the categories associated with the current Provocare
            SelectedCategories = await _context.CategorieProvocare
                .Where(cp => cp.ProvocareId == provocare.Id)
                .Select(cp => cp.CategorieId)
                .ToListAsync();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Provocare).State = EntityState.Modified;

            // Add the newly selected categories

            foreach (var categoryId in SelectedCategories)
            {
                var categ = await _context.Categorie.FindAsync(categoryId);
                if (categ != null)
                {
                    var categorieProvocari = new CategorieProvocare
                    {
                        Provocare = Provocare,
                        Categorie = categ
                    };
                    _context.CategorieProvocare.Add(categorieProvocari);
                }
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvocareExists(Provocare.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./IndexAdmin");
        }

        private bool ProvocareExists(int id)
        {
          return (_context.Provocare?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
