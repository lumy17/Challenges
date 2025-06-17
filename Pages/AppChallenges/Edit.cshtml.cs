using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.WebApp.Pages.AppChallenges
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Challenge Challenge { get; set; } = default!;

        public List<Challenge> Challenges { get; set; }

        public List<Category> Categories { get; set; }

        [BindProperty]
        public List<int> SelectedCategories { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Challenge == null)
            {
                return NotFound();
            }

            var challenge =  await _context.Challenge.FirstOrDefaultAsync(m => m.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }

            Challenge = challenge;

            Challenges = await _context.Challenge.ToListAsync();
            Categories = await _context.Category.ToListAsync();

            SelectedCategories = await _context.ChallengeCategory
                .Where(cp => cp.ChallengeId == challenge.Id)
                .Select(cp => cp.CategoryId)
                .ToListAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Categories = await _context.Category.ToListAsync();

            _context.Attach(Challenge).State = EntityState.Modified;
			_context.ChallengeCategory.RemoveRange(_context.ChallengeCategory.Where(cp => cp.ChallengeId == Challenge.Id));

			foreach (var categoryId in SelectedCategories)
			{
				var category = Categories.FirstOrDefault(c => c.Id == categoryId);
				if (category != null)
				{
					var challengeCategory = new ChallengeCategory
					{
                        Challenge = Challenge,
						Category = category
					};
					_context.ChallengeCategory.Add(challengeCategory);
				}
			}
			try
			{
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeExists(Challenge.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./IndexAdmin");
        }

        private bool ChallengeExists(int id)
        {
          return (_context.Challenge?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
