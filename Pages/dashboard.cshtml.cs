using Challenges.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using Challenges.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Pages
{
    public class dashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public int FinishedChallengesCount { get; set; }
        public int Streak { get; set; }
        public int Puncte { get; set; }
        public string currentUser { get; set; }
        public List<Realizare> Badgeuri { get; set; }
        public List<Sarcina> SarciniCurente { get; set; } = new List<Sarcina>();
        public List<Provocare> ListaProvocari { get; set; }

        public dashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            ListaProvocari = _context.Provocare.ToList();
            currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault
                (u => u.Email == currentUser);
            if (user != null)
            {
                FinishedChallengesCount = _context.ProvocareUtilizator
                .Count(p => p.UtilizatorId == user.Id
                && p.Stare == "finalizat");

                Streak = user.Streak;
                Puncte = user.Puncte;

                Badgeuri = _context.Realizare.ToList();
                var provocareUser = _context.ProvocareUtilizator.FirstOrDefault
                    (u => u.UtilizatorId == user.Id);
                //verifica daca utilizatorul curent a realizat o sarcina adica
                //daca exista o data_realizare cu data de azi pentru
                //o provocare specifica a unui utilizator
                if (provocareUser != null)
                {
                    bool sarcinaFinalizataAzi = _context.SarcinaRealizata
                        .Any(u => u.ProvocareUtilizatorId == provocareUser.Id
                        && u.Data_Realizare.Value.Date == DateTime.Today);
                    // Verifica daca utilizatorul a finalizat o sarcina ieri
                    bool sarcinaFinalizataIeri = _context.SarcinaRealizata
                        .Any(u => u.ProvocareUtilizatorId == provocareUser.Id
                        && u.Data_Realizare.Value.Date == DateTime.Today.AddDays(-1));

                    if (sarcinaFinalizataAzi && user.DataUltimaActualizareStreak != DateTime.Today)
                    {
                        user.Streak++;
                        user.DataUltimaActualizareStreak = DateTime.Today;

                        _context.Update(user);
                        _context.SaveChanges();
                    }
                    // Daca utilizatorul nu a finalizat o sarcina ieri, reseteaza streak-ul la 0
                    else if (!sarcinaFinalizataIeri && !sarcinaFinalizataAzi)
                    {
                        user.Streak = 0;

                        _context.Update(user);
                        _context.SaveChanges();
                    }
                }
                Streak = user.Streak;

                var provocariUtilizator = _context.ProvocareUtilizator
    .Where(pu => pu.UtilizatorId == user.Id && pu.Stare != "finalizat")
    .ToList();
                if (provocariUtilizator.Count != 0)
                {
                    bool sarcinaFinalizataAzi = _context.SarcinaRealizata
    .Any(u => u.ProvocareUtilizatorId == provocareUser.Id
    && u.Data_Realizare.Value.Date == DateTime.Today);
                    foreach (var prov in provocariUtilizator)
                    {
                        if (sarcinaFinalizataAzi)
                        {
                            var sarcinaCurenta = _context.Sarcina.FirstOrDefault(s =>
    s.ProvocareId == prov.ProvocareId
    && s.Ziua == prov.ZiuaCurenta-1);
                            if (sarcinaCurenta != null)
                            {
                                SarciniCurente.Add(sarcinaCurenta);
                            }
                        }
                        else {
                            var sarcinaCurenta = _context.Sarcina.FirstOrDefault(s =>
        s.ProvocareId == prov.ProvocareId
        && s.Ziua == prov.ZiuaCurenta);
                            if (sarcinaCurenta != null)
                            {
                                SarciniCurente.Add(sarcinaCurenta);
                            }
                        }
                    }
                }
            }
        }
        public bool UserHasBadge(int badgeId)
        {
            var user = _context.Utilizator.FirstOrDefault(u => u.Email == currentUser);
            return _context.RealizareUtilizator.Any(ru => ru.UtilizatorId == user.Id && ru.RealizareId == badgeId);
        }
    }
}
