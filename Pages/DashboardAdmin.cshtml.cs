using Challenges.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Challenges.Pages
{
    public class DashboardAdminModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardAdminModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public int InregistrariUtilizatori { get; set; }
        public int ProvocariIncepute { get; set; }
        public int ProvocariFinalizate { get; set; }
        public List<string> Activitati { get; set; } = new List<string>();
        public async Task OnGetAsync()
        {
            InregistrariUtilizatori = await _context.Utilizator.CountAsync();
            ProvocariIncepute = await _context.ProvocareUtilizator.CountAsync
                (pu => pu.Data_start != null);
            ProvocariFinalizate = await _context.ProvocareUtilizator.CountAsync
                (pu => pu.Stare == "finalizat");

            var ultimeleProvocariIncepute = await _context.ProvocareUtilizator
            .Where(pu => pu.Data_start != null && pu.Stare != "finalizat")
            .OrderByDescending(pu => pu.Data_start)
            .ToListAsync();

            var ultimeleProvocariFinalizate = await _context.ProvocareUtilizator
                .Where(pu => pu.Stare == "finalizat")
                .OrderByDescending(pu => pu.DataFinal)
                .ToListAsync();

            var ultimeleSarciniFinalizate = await _context.SarcinaRealizata
                .OrderByDescending(sr => sr.Data_Realizare)
                .ToListAsync();

            Activitati = ultimeleProvocariFinalizate.Select(pu =>
                $"Utilizatorul {_context.Utilizator.Find(pu.UtilizatorId).Nume} " +
                $"a finalizat provocarea \"{_context.Provocare.Find(pu.ProvocareId).Nume}\" " +
                $"acum {(DateTime.Now - pu.DataFinal.Value).TotalMinutes} minute"
            ).Concat(ultimeleSarciniFinalizate.Select(sr =>
                $"Utilizatorul {_context.Utilizator.Find(_context.ProvocareUtilizator.Find(sr.ProvocareUtilizatorId).UtilizatorId).Nume} " 
                + $"a finalizat sarcina \"{_context.Sarcina.Find(sr.SarcinaId).Nume}\" " +
                $"acum {Math.Floor((DateTime.Now - sr.Data_Realizare.Value).TotalMinutes)} minute"
)).Concat(ultimeleProvocariIncepute.Select(pu =>
            $"Utilizatorul {_context.Utilizator.Find(pu.UtilizatorId).Nume} " +
            $"a început provocarea \"{_context.Provocare.Find(pu.ProvocareId).Nume}\" " +
            $"acum {Math.Floor((DateTime.Now - pu.Data_start.Value).TotalMinutes)} minute"
        )).OrderByDescending(s => s.Split("acum ")[1].Split(" minute")[0]).ToList();
        }
    }
}
