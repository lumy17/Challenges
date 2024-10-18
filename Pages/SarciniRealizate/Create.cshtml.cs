using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenges.WebApp.Pages.SarciniRealizate
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["IdProvocareUtilizator"] = new SelectList(_context.ProvocareUtilizator, "Id", "Id");
            ViewData["IdSarcini"] = new SelectList(_context.Sarcina, "Id", "Id");
            ListaProvocari = _context.Provocare.ToList();
            return Page();
        }

        [BindProperty]
        public SarcinaRealizata SarcinaRealizata { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ProvocareUtilizator provocareUtilizator = await _context.ProvocareUtilizator.FindAsync(SarcinaRealizata.ProvocareUtilizatorId);
            Sarcina sarcina = await _context.Sarcina.FindAsync(SarcinaRealizata.SarcinaId);

            // Verificați dacă provocarea și utilizatorul au fost găsite în baza de date
            if (provocareUtilizator == null || sarcina == null)
            {
                return NotFound(); // Sau gestionați în alt mod situația în care nu se găsesc
            }

            //se asociaza obiectele reale, nu doar idurile
            SarcinaRealizata.Sarcina = sarcina;
            SarcinaRealizata.ProvocareUtilizator = provocareUtilizator;

            _context.SarcinaRealizata.Add(SarcinaRealizata);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
