using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.ProvocariUtilizatori
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProvocareId"] = new SelectList(_context.Provocare, "Id", "Id");
        ViewData["UtilizatorId"] = new SelectList(_context.Utilizator, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ProvocareUtilizator ProvocareUtilizator { get; set; }
        
        //pentru a evita modelstate invalid a trebui sa atribui valori si obiectelor
        //provocare si utilizator. astfel, le-am cautat in bd in functie de id si dupa
        //le-am salvat in bd.
        public async Task<IActionResult> OnPostAsync()
        {
            // Obțineți provocarea și utilizatorul asociat pe baza ID-urilor trimise din formular
            Provocare provocare = await _context.Provocare.FindAsync(ProvocareUtilizator.ProvocareId);
            Utilizator utilizator = await _context.Utilizator.FindAsync(ProvocareUtilizator.UtilizatorId);

            // Verificați dacă provocarea și utilizatorul au fost găsite în baza de date
            if (provocare == null || utilizator == null)
            {
                return NotFound(); // Sau gestionați în alt mod situația în care nu se găsesc
            }

            //se asociaza obiectele reale, nu doar idurile
            ProvocareUtilizator.Provocare = provocare;
            ProvocareUtilizator.Utilizator = utilizator;

            _context.ProvocareUtilizator.Add(ProvocareUtilizator);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
