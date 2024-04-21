﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;
using Microsoft.AspNetCore.Authorization;

namespace Challenges.Pages.ProvocariUtilizatori
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public IndexModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ProvocareUtilizator> ProvocareUtilizator { get;set; } = default!;
        public List<Provocare> ListaProvocari { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.ProvocareUtilizator != null)
            {
                ProvocareUtilizator = await _context.ProvocareUtilizator
                .Include(p => p.Provocare)
                .Include(p => p.Utilizator).ToListAsync();
            }
            ListaProvocari = await _context.Provocare.ToListAsync();

        }
    }
}
