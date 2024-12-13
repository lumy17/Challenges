using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.FinishedTasks
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public FinishedTask FinishedTask { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FinishedTask == null)
            {
                return NotFound();
            }
            Challenges = await _context.Challenge.ToListAsync();


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
    }
}
