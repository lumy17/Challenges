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

namespace Challenges.WebApp.Pages.Utilizatori
{
    public class EditModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public EditModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppUser AppUser { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<int> SelectedCategories { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AppUser == null)
            {
                return NotFound();
            }

            var utilizator = await _context.AppUser
                  .Include(u => u.UserCategories)
                  .ThenInclude(cu => cu.Category)
                  .FirstOrDefaultAsync(m => m.Id == id); Challenges = await _context.Challenge.ToListAsync();

            if (utilizator == null)
            {
                return NotFound();
            }
            AppUser = utilizator;
            Categories = await _context.Category.ToListAsync();
            SelectedCategories = utilizator.UserCategories.Select(cu => cu.Category.Id).ToList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _context.Category.ToListAsync();
                return Page();
            }

            _context.Attach(AppUser).State = EntityState.Modified;

            // Remove existing category associations
            var existingUserPreference = await _context.UserPreference
                .Where(cu => cu.AppUserId == AppUser.Id)
                .ToListAsync();
            _context.UserPreference.RemoveRange(existingUserPreference);

            // Add new category associations
            foreach (var categoryId in SelectedCategories)
            {
                var category = await _context.Category.FindAsync(categoryId);
                if (category != null)
                {
                    var userPreference = new UserPreference
                    {
                        AppUser = AppUser,
                        Category = category
                    };
                    _context.UserPreference.Add(userPreference);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilizatorExists(AppUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UtilizatorExists(int id)
        {
          return (_context.AppUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
