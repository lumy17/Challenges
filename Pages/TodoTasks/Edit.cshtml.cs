using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.TodoTasks
{
    public class EditModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public EditModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TodoTask TodoTask { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TodoTask == null)
            {
                return NotFound();
            }

            var sarcina =  await _context.TodoTask.FirstOrDefaultAsync(m => m.Id == id);
            if (sarcina == null)
            {
                return NotFound();
            }
            TodoTask = sarcina;
                 Challenges = await _context.Challenge.ToListAsync();
            ViewData["ChallengeId"] = new SelectList(_context.Challenge, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TodoTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoTaskExists(TodoTask.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

			return RedirectToPage("./Index", new { id = TodoTask.ChallengeId });
		}

		private bool TodoTaskExists(int id)
        {
          return (_context.TodoTask?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
