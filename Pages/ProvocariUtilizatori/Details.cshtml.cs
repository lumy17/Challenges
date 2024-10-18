using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.WebApp.Pages.ProvocariUtilizatori
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public IList<ProvocareUtilizator> ProvocareUtilizator { get; set; }
        public List<Provocare> ListaProvocari { get; set; }

        public async Task OnGetAsync()
        {
            ListaProvocari = await _context.Provocare.ToListAsync();
            var currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault
                        (u => u.Email == currentUser);
            if (user != null)
            {
                ProvocareUtilizator = await _context.ProvocareUtilizator
                    .Include(p => p.Provocare)
                    .Where(u => u.UtilizatorId == user.Id)
                    .ToListAsync();
            }

        }
    }
}
