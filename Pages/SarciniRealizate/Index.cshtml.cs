using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;
using MessagePack;

namespace Challenges.Pages.SarciniRealizate
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SarcinaRealizata> SarcinaRealizata { get;set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }

        public async Task OnGetAsync(int? provocareUtilId)
        {
            if (provocareUtilId.HasValue)
            {
                SarcinaRealizata = await _context.SarcinaRealizata
                    .Where(sr => sr.ProvocareUtilizatorId == provocareUtilId.Value)
                    .ToListAsync();
            }
            else
            {
                SarcinaRealizata = await _context.SarcinaRealizata.ToListAsync();
            }
            ListaProvocari = await _context.Provocare.ToListAsync();

        }
    }
}
