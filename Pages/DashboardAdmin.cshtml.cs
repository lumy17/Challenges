using Challenges.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Challenges.Pages
{
    [Authorize(Roles = "Admin")]
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
			{
				var utilizator = _context.Utilizator.Find(pu.UtilizatorId).Nume;
				var provocare = _context.Provocare.Find(pu.ProvocareId).Nume;
				var interval = DateTime.Now - pu.DataFinal.Value;
				var formattedInterval = FormatTimeInterval(interval);
				return $"Utilizatorul {utilizator} a finalizat provocarea \"{provocare}\" {formattedInterval}";
			}
).Concat(ultimeleSarciniFinalizate.Select(sr =>
{
	var utilizator = _context.Utilizator.Find(_context.ProvocareUtilizator.Find(sr.ProvocareUtilizatorId).UtilizatorId).Nume;
	var sarcina = _context.Sarcina.Find(sr.SarcinaId).Nume;
	var interval = DateTime.Now - sr.Data_Realizare.Value;
	var formattedInterval = FormatTimeInterval(interval);
	return $"Utilizatorul {utilizator} a finalizat sarcina \"{sarcina}\" {formattedInterval}";
}
)).Concat(ultimeleProvocariIncepute.Select(pu =>
{
	var utilizator = _context.Utilizator.Find(pu.UtilizatorId).Nume;
	var provocare = _context.Provocare.Find(pu.ProvocareId).Nume;
	var interval = DateTime.Now - pu.Data_start.Value;
	var formattedInterval = FormatTimeInterval(interval);
	return $"Utilizatorul {utilizator} a început provocarea \"{provocare}\" {formattedInterval}";
}
)).OrderBy(s => {
	var splitParts = s.Split("acum ");
	return splitParts.Length >= 2 ? splitParts[1].Split(" luni")[0] : "";
}).ToList();

		}

		private string FormatTimeInterval(TimeSpan interval)
		{
			var formattedInterval = "";
			if ((int)interval.TotalDays / 30 > 0)
				formattedInterval += $"{(int)interval.TotalDays / 30} luni ";
			if ((int)interval.TotalDays % 30 > 0)
				formattedInterval += $"{(int)interval.TotalDays % 30} zile ";
			if ((int)interval.TotalHours % 24 > 0)
				formattedInterval += $"{(int)interval.TotalHours % 24} ore ";
			if ((int)interval.TotalMinutes % 60 > 0)
				formattedInterval += $"{(int)interval.TotalMinutes % 60} minute ";
			if ((int)interval.TotalSeconds % 60 > 0)
				formattedInterval += $"{(int)interval.TotalSeconds % 60} secunde ";
			if (!string.IsNullOrEmpty(formattedInterval))
			{
				formattedInterval = "acum " + formattedInterval;
			}
			return formattedInterval.Trim();
		}
	}
}
