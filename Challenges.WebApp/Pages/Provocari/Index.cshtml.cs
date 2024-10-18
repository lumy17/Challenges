using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.Provocari
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Provocare> ListaProvocari { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Provocare != null)
            {
                ListaProvocari= await _context.Provocare.ToListAsync();
            }
        }
    }
}
