﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.Realizari
{
    public class IndexModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Realizare> Realizare { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Realizare != null)
            {
                Realizare = await _context.Realizare.ToListAsync();
            }
        }
    }
}
