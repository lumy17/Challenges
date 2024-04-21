using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Provocari
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Provocare Provocare { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Provocare == null)
            {
                return NotFound();
            }
            var provocare = await _context.Provocare
                .FirstOrDefaultAsync(m => m.Id == id);
            ListaProvocari = await _context.Provocare.ToListAsync();
            if (provocare == null)
            {
                return NotFound();
            }
            else
            {
                Provocare = provocare;
            }
            var currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault(u => u.Email == currentUser);
            //de fiecare data cand un vizitator da click pe o provocare
            //vizulizarile vor creste
            Provocare.Vizualizari++;
            await _context.SaveChangesAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Provocare = await _context.Provocare.FirstOrDefaultAsync(m => m.Id == id);
            if (Provocare == null)
            {
                return NotFound();
            }
            var currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault(u => u.Email == currentUser);
            if (user != null)
            {

                var provocareUtilizator = new ProvocareUtilizator
                {
                    ProvocareId = Provocare.Id,
                    UtilizatorId = user.Id,
                    Data_start = DateTime.Now,
                    DataFinal = DateTime.Now.AddDays(Provocare.Durata),
                    ZiuaCurenta = 1,
                    Stare = "In desfasurare"
                };
                _context.ProvocareUtilizator.Add(provocareUtilizator);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Tasks", new { id = Provocare.Id });
            }

            return Page();
        }
    }
}