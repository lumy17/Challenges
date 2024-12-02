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
        [BindProperty]
        public FinishedTask FinishedTask { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }

        public IActionResult OnGet()
        {
            ViewData["IdUserChallenge"] = new SelectList(_context.UserChallenge, "Id", "Id");
            ViewData["IdTodoTask"] = new SelectList(_context.TodoTask, "Id", "Id");
            Challenges = _context.Challenge.ToList();
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            UserChallenge userChallenge = await _context.UserChallenge.FindAsync(FinishedTask.UserChallengeId);
            TodoTask todoTask = await _context.TodoTask.FindAsync(FinishedTask.TodoTaskId);

            // Verificați dacă provocarea și utilizatorul au fost găsite în baza de date
            if (userChallenge == null || todoTask == null)
            {
                return NotFound(); // Sau gestionați în alt mod situația în care nu se găsesc
            }

            //se asociaza obiectele reale, nu doar idurile
            FinishedTask.TodoTask = todoTask;
            FinishedTask.UserChallenge = userChallenge;

            _context.FinishedTask.Add(FinishedTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
