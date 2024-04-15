using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.CategoriiUtilizatori
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CategorieUtilizator> CategorieUtilizator { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }


        public async Task OnGetAsync()
        {
            if (_context.CategorieUtilizator != null)
            {
                CategorieUtilizator = await _context.CategorieUtilizator
                .Include(c => c.Categorie)
                .Include(c => c.Utilizator).ToListAsync();

                ListaProvocari = await _context.Provocare.ToListAsync();

            }
        }
    }
}
