using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.Data;
using Challenges.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Pages.Provocari
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            ListaProvocari = await _context.Provocare.ToListAsync();
            ListaCategorii = await _context.Categorie.ToListAsync();   

            return Page();
        }

        [BindProperty]
        public Provocare Provocare { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }

        public List<Categorie> ListaCategorii { get; set; }
        [BindProperty]
        public List<int> SelectedCategories { get; set; } = new List<int>();

        public async Task<IActionResult> OnPostAsync()
        {
          foreach(var categId in SelectedCategories)
            {
                var categ = await _context.Categorie.FindAsync(categId);
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

            _context.Provocare.Add(Provocare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
