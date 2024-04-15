using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.CategoriiProvocari
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CategorieProvocare> CategorieProvocare { get;set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.CategorieProvocare != null)
            {
                CategorieProvocare = await _context.CategorieProvocare
                .Include(c => c.Categorie)
                .Include(c => c.Provocare).ToListAsync();
            }
            ListaProvocari = await _context.Provocare.ToListAsync();

        }
    }
}
