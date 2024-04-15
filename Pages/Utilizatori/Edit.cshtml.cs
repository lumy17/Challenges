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

namespace Challenges.Pages.Utilizatori
{
    public class EditModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public EditModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Utilizator Utilizator { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }
        public List<Categorie> ListaCategorii { get; set; }
        [BindProperty]
        public List<int> SelectedCategories { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Utilizator == null)
            {
                return NotFound();
            }

            var utilizator = await _context.Utilizator
                  .Include(u => u.CategoriiUtilizatori)
                  .ThenInclude(cu => cu.Categorie)
                  .FirstOrDefaultAsync(m => m.Id == id); ListaProvocari = await _context.Provocare.ToListAsync();

            if (utilizator == null)
            {
                return NotFound();
            }
            Utilizator = utilizator;
            ListaCategorii = await _context.Categorie.ToListAsync();
            SelectedCategories = utilizator.CategoriiUtilizatori.Select(cu => cu.Categorie.Id).ToList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ListaCategorii = await _context.Categorie.ToListAsync();
                return Page();
            }

            _context.Attach(Utilizator).State = EntityState.Modified;

            // Remove existing category associations
            var existingCategorieUtilizator = await _context.CategorieUtilizator
                .Where(cu => cu.UtilizatorId == Utilizator.Id)
                .ToListAsync();
            _context.CategorieUtilizator.RemoveRange(existingCategorieUtilizator);

            // Add new category associations
            foreach (var categoryId in SelectedCategories)
            {
                var category = await _context.Categorie.FindAsync(categoryId);
                if (category != null)
                {
                    var categorieUtilizator = new CategorieUtilizator
                    {
                        Utilizator = Utilizator,
                        Categorie = category
                    };
                    _context.CategorieUtilizator.Add(categorieUtilizator);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilizatorExists(Utilizator.Id))
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

        private bool UtilizatorExists(int id)
        {
          return (_context.Utilizator?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
