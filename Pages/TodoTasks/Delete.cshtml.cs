﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.TodoTasks
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TodoTask TodoTask { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TodoTask == null)
            {
                return NotFound();
            }

            var todoTask = await _context.TodoTask.FirstOrDefaultAsync(m => m.Id == id);

            if (todoTask == null)
            {
                return NotFound();
            }
            else 
            {
                TodoTask = todoTask;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TodoTask == null)
            {
                return NotFound();
            }
            var todoTask = await _context.TodoTask.FindAsync(id);

            if (todoTask != null)
            {
                TodoTask = todoTask;
                _context.TodoTask.Remove(TodoTask);
                await _context.SaveChangesAsync();
            }

			return RedirectToPage("./Index", new { id = TodoTask.ChallengeId });
		}
	}
}
