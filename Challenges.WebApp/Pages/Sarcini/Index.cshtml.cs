using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.Sarcini
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Provocare> ListaProvocari { get; set; }
                    public Provocare Provocare { get; set; }

        public IList<Sarcina> Sarcina { get;set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            Provocare = await _context.Provocare
                            .Include(p => p.Sarcini)
                            .FirstOrDefaultAsync(p => p.Id == id);

			if (Provocare != null)
			{
				Sarcina = Provocare.Sarcini.ToList();
			}

			ListaProvocari = await _context.Provocare.ToListAsync();
        }
    }
}
