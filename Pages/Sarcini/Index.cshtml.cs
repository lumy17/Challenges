using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Sarcini
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.Data.ApplicationDbContext context)
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

            Sarcina = Provocare.Sarcini.ToList();
            ListaProvocari = await _context.Provocare.ToListAsync();
        }
    }
}
