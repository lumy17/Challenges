﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.WebApp.Data;
using Challenges.WebApp.Models;

namespace Challenges.WebApp.Pages.Sarcini
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.WebApp.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public TodoTask TodoTask { get; set; } = default!;
        public List<Challenge> Challenges { get; set; }

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
            Challenges = await _context.Challenge.ToListAsync();

            return Page();
        }
    }
}
