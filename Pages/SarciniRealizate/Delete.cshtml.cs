﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.SarciniRealizate
{
    public class DeleteModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public FinishedTask FinishedTask { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FinishedTask == null)
            {
                return NotFound();
            }

            var finishedTask = await _context.FinishedTask.FirstOrDefaultAsync(m => m.Id == id);

            if (finishedTask == null)
            {
                return NotFound();
            }
            else 
            {
                FinishedTask = finishedTask;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FinishedTask == null)
            {
                return NotFound();
            }
            var sarcinarealizata = await _context.FinishedTask.FindAsync(id);

            if (sarcinarealizata != null)
            {
                FinishedTask = sarcinarealizata;
                _context.FinishedTask.Remove(FinishedTask);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
