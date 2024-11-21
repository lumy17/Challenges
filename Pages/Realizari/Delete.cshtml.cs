﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.Realizari
{
    public class DeleteModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Badge Badge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Badge == null)
            {
                return NotFound();
            }

            var badge = await _context.Badge.FirstOrDefaultAsync(m => m.Id == id);

            if (badge == null)
            {
                return NotFound();
            }
            else 
            {
                Badge = badge;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Badge == null)
            {
                return NotFound();
            }
            var badge = await _context.Badge.FindAsync(id);

            if (badge != null)
            {
                Badge = badge;
                _context.Badge.Remove(Badge);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}