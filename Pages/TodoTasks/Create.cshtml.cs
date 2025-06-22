using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.TodoTasks
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ChallengeId"] = new SelectList(_context.Challenge, "Id", "Id");

            return Page();
        }

        [BindProperty]
        public TodoTask TodoTask { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || TodoTask == null)
            {
                return Page();
            }

            _context.TodoTask.Add(TodoTask);
            await _context.SaveChangesAsync();

			return RedirectToPage("./Index", new { id = TodoTask.ChallengeId });
		}
    }
}
