using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.TodoTasks
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Challenge> Challenges { get; set; }
                    public Challenge Challenge { get; set; }

        public IList<TodoTask> TodoTasks { get;set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            Challenge = await _context.Challenge
                            .Include(p => p.TodoTasks)
                            .FirstOrDefaultAsync(p => p.Id == id);

			if (Challenge != null)
			{
				TodoTasks = Challenge.TodoTasks.ToList();
			}

            Challenges = await _context.Challenge.ToListAsync();
        }
    }
}
