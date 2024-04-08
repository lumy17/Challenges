﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;

namespace Challenges.Pages.ProvocariUtilizatori
{
    public class DetailsModel : PageModel
    {
        private readonly Challenges.Data.ApplicationDbContext _context;

        public DetailsModel(Challenges.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public IList<ProvocareUtilizator> ProvocareUtilizator { get; set; }

        public async Task OnGetAsync()
        {
            var currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault
                        (u => u.Email == currentUser);
            if (user != null)
            {
                ProvocareUtilizator = await _context.ProvocareUtilizator
                    .Include(p => p.Provocare)
                    .Where(u => u.UtilizatorId == user.Id)
                    .ToListAsync();
            }

        }
    }
}
