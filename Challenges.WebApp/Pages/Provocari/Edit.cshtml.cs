using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.WebApp.Pages.Provocari
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public EditModel(Challenges.WebApp.Data.ApplicationDbContext context)
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
            ListaCategorii = await _context.Categorie.ToListAsync();

            _context.Attach(Provocare).State = EntityState.Modified;
			_context.CategorieProvocare.RemoveRange(_context.CategorieProvocare.Where(cp => cp.ProvocareId == Provocare.Id));


			// Add the newly selected categories

			foreach (var categoryId in SelectedCategories)
			{
				var categ = ListaCategorii.FirstOrDefault(c => c.Id == categoryId);
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
