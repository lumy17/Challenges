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

namespace Challenges.WebApp.Pages.Sarcini
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Challenge> Challenges { get; set; }
        public IActionResult OnGet()
        {
        ViewData["ProvocareId"] = new SelectList(_context.Challenge, "Id", "Id");
            Challenges = _context.Challenge.ToList();

            return Page();
        }

        [BindProperty]
        public TodoTask TodoTask { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TodoTask == null || TodoTask == null)
            {
                return Page();
            }

            _context.TodoTask.Add(TodoTask);
            await _context.SaveChangesAsync();

			return RedirectToPage("./Index", new { id = TodoTask.ChallengeId });
		}
    }
}
