using Challenges.Extensions;
using Challenges.WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Challenges.WebApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class DashboardAdminModel : PageModel
    {
        /* TODOS:
         * Move logic to Business layer
         * Create a ViewModel with the 4 Properties, and return from Business layer
         * Use English language
         * Pascal Case for Property naming - FinishDate instead of Data_Realizare
         *  Constant value (or enum) for "finalizat"
         */

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
            await PopulateAdminDashboard();
        }

        private async Task PopulateAdminDashboard()
        {
            InregistrariUtilizatori = await _context.Utilizator.CountAsync();
            ProvocariIncepute = await _context.ProvocareUtilizator.CountAsync
                (pu => pu.Data_start != null);
            ProvocariFinalizate = await _context.ProvocareUtilizator.CountAsync
                (pu => pu.Stare == "finalizat");
            Activitati = await GetActivitati();
        }

        private async Task<List<string>> GetActivitati()
        {
            var finalList = (await GetUltimeleProvocariFinalizate())
                .Concat(await GetUltimeleSarciniFinalizate())
                // .Concat(await GetUltimeleProvocariIncepute())
                .OrderBy(s =>
                {
                    var splitParts = s.Split("acum ");
                    return splitParts.Length >= 2 ? splitParts[1].Split(" luni")[0] : "";
                })
                .ToList();

            return finalList;
        }

        private async Task<List<string>> GetUltimeleProvocariFinalizate()
        {
            var ultimeleProvocariFinalizate = await _context.ProvocareUtilizator
                .Where(pu => pu.Stare == "finalizat")
                // Eager loading - will generate sql joins and return data from multiple tables
                // Otherwise if we call Db inside a Select it will cause lots of Db calls for each main entity
                .Include(pu => pu.Utilizator)
                .Include(pu => pu.Provocare)
                .OrderByDescending(pu => pu.DataFinal)
                // We only return 1 field from each of 3 tables involved in join
                // Otherwise all fields are being returned
                .Select(pu => new
                        {
                            Utilizator = pu.Utilizator.Nume,
                            Provocare = pu.Provocare.Nume,
                            DataFinal = pu.DataFinal
                        }
                )
                // Everything above this will be executed directly at Database level
                .ToListAsync();

            // Alternative:
            /*
            var ultimeleProvocariFinalizate2 = await _context.ProvocareUtilizator
                .Where(pu => pu.Stare == "finalizat").ToListAsync();
            var utilizatoriIds = ultimeleProvocariFinalizate2.Select(x => x.UtilizatorId).ToHashSet();
            var provocariIds = ultimeleProvocariFinalizate2.Select(x => x.ProvocareId).ToHashSet();
            var utilizatori = _context.Utilizator.Where(x => utilizatoriIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
            var provocari = _context.Utilizator.Where(x => provocariIds.Contains(x.Id));

            var final = ultimeleProvocariFinalizate2.Select(x => new
            {
                Utilizator = utilizatori[x.UtilizatorId].Nume
            })
            */

            // We have another select, because EF doesn't know to generate SQL code from FormatTimeInterval() method 
            // Which we manually defined
            // This is done in memory (not ad Db level)
            return ultimeleProvocariFinalizate.Select(pu =>
            {
                var interval = DateTime.Now - pu.DataFinal.Value;
                var formattedInterval = interval.FormatTimeInterval();
                return $"Utilizatorul {pu.Utilizator} a finalizat provocarea \"{pu.Provocare}\" {formattedInterval}";
            }).ToList();
        }

        private async Task<List<string>> GetUltimeleSarciniFinalizate()
        {
            var ultimeleSarciniFinalizate = await _context.SarcinaRealizata
                .OrderByDescending(sr => sr.Data_Realizare)
                .ToListAsync();
            return ultimeleSarciniFinalizate.Select(sr =>
            {
                var utilizator = _context.Utilizator
                    .Find(_context.ProvocareUtilizator.Find(sr.ProvocareUtilizatorId).UtilizatorId).Nume;
                var sarcina = _context.Sarcina.Find(sr.SarcinaId).Nume;
                var interval = DateTime.Now - sr.Data_Realizare.Value;
                var formattedInterval = interval.FormatTimeInterval();
                return $"Utilizatorul {utilizator} a finalizat sarcina \"{sarcina}\" {formattedInterval}";
            }).ToList();
        }

        private async Task<List<string>> GetUltimeleProvocariIncepute()
        {
            var ultimeleProvocariIncepute = await _context.ProvocareUtilizator
                .Where(pu => pu.Data_start != null && pu.Stare != "finalizat")
                .OrderByDescending(pu => pu.Data_start)
                .ToListAsync();
            return ultimeleProvocariIncepute.Select(pu =>
                {
                    var utilizator = _context.Utilizator.Find(pu.UtilizatorId).Nume;
                    var provocare = _context.Provocare.Find(pu.ProvocareId).Nume;
                    var interval = DateTime.Now - pu.Data_start.Value;
                    var formattedInterval = interval.FormatTimeInterval();
                    return $"Utilizatorul {utilizator} a început provocarea \"{provocare}\" {formattedInterval}";
                }
            ).ToList();
        }

    }
}
