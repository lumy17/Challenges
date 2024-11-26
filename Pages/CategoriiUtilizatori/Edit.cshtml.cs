﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.CategoriiUtilizatori
{
    public class EditModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public EditModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategorieUtilizator CategorieUtilizator { get; set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CategorieUtilizator == null)
            {
                return NotFound();
            }

            var categorieutilizator =  await _context.CategorieUtilizator.FirstOrDefaultAsync(m => m.Id == id);

            ListaProvocari = await _context.Provocare.ToListAsync();

            if (categorieutilizator == null)
            {
                return NotFound();
            }
            CategorieUtilizator = categorieutilizator;
           ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id");
           ViewData["UtilizatorId"] = new SelectList(_context.Utilizator, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CategorieUtilizator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieUtilizatorExists(CategorieUtilizator.Id))
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

        private bool CategorieUtilizatorExists(int id)
        {
          return (_context.CategorieUtilizator?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}