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

        public IList<Sarcina> Sarcina { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Sarcina != null)
            {
                Sarcina = await _context.Sarcina
                .Include(s => s.Provocare).ToListAsync();
            }
        }
    }
}
