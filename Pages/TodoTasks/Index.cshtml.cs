using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.TodoTasks
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Challenge Challenge { get; set; }

        public List<TodoTask> TodoTasks { get;set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            Challenge = await _context.Challenge
                .Include(p => p.TodoTasks)
                .FirstOrDefaultAsync(p => p.Id == id);

			if (Challenge != null)
			{
				TodoTasks = Challenge.TodoTasks.ToList();
			}
        }
    }
}
