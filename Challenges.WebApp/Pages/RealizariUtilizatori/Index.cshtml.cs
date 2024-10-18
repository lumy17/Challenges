using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.RealizariUtilizatori
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RealizareUtilizator> RealizareUtilizator { get;set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.RealizareUtilizator != null)
            {
                RealizareUtilizator = await _context.RealizareUtilizator
                .Include(r => r.Realizare)
                .Include(r => r.Utilizator).ToListAsync();
            }
            ListaProvocari = await _context.Provocare.ToListAsync();

        }
    }
}
