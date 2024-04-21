﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Challenges.Data;
using Challenges.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Pages.Realizari
{
    public class CreateModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public CreateModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ListaProvocari = _context.Provocare.ToList();

            return Page();
        }

        [BindProperty]
        public Realizare Realizare { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Realizare == null || Realizare == null)
            {
                return Page();
            }

            _context.Realizare.Add(Realizare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}