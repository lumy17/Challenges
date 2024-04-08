using Challenges.Data;
using Challenges.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Challenges.Pages.Provocari
{
    public class TasksModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public TasksModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public Provocare Provocare { get; set; } = default!;

        public List<Sarcina> Sarcini { get; set; } = new List<Sarcina>();

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        //same as totalpages 
        public int Count { get; set; }
        public int PageSize { get; set; } = 1;
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < Count;

        public async Task<IActionResult> OnGetAsync(int? id, int? currentpage)
        {
            if (id == null || _context.Provocare == null)
            {
                return NotFound();
            }
            var provocare = await _context.Provocare.FirstOrDefaultAsync(m => m.Id == id);
            if (provocare == null)
            {
                return NotFound();
            }
            else
            {
                Provocare = provocare;
                var allSarcini = await _context.Sarcina.Where(
                   s => s.ProvocareId == id).ToListAsync();
                // daca intra prima data pe pagina de sarcini currentpage ia valoarea 1
                CurrentPage = currentpage ?? 1;
                Sarcini = allSarcini.OrderBy(d => d.Ziua).Skip(CurrentPage - 1).Take(PageSize).ToList();

            }
            Count = Provocare.Durata;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Provocare = await _context.Provocare.FirstOrDefaultAsync
                (m => m.Id == id);
            var currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault
                (u => u.Email == currentUser);
            if (Provocare == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostMarkTaskAsCompletedAsync(int idTask, int idProv)
        {
            var currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault
                (u => u.Email == currentUser);
            Provocare = await _context.Provocare.FirstOrDefaultAsync
                (m => m.Id == idProv);

            //gaseste provocarea utilizatorului si sarcina
            var provocareUtilizator = _context.ProvocareUtilizator.
                FirstOrDefault(pu => pu.UtilizatorId == user.Id 
                && pu.ProvocareId == Provocare.Id);
            var sarcina = _context.Sarcina.FirstOrDefault(s => s.Id == idTask);

            //creaza o noua inregistrare in SarcinaRealizata
            var sarcinaRealizata = new SarcinaRealizata
            {
                ProvocareUtilizatorId = provocareUtilizator.Id,
                SarcinaId = sarcina.Id,
                Data_Realizare = DateTime.Today,
                ZiuaRealizare = sarcina.Ziua
            };
            _context.SarcinaRealizata.Add(sarcinaRealizata);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostMarkChallengeAsCompletedAsync(int id)
        {
            var currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault
                (u => u.Email == currentUser);

            //gaseste provocarea utilizatorului
            var provocareUtilizator = _context.ProvocareUtilizator.
                FirstOrDefault(pu => pu.UtilizatorId == user.Id 
                && pu.ProvocareId == id);

            //marcheaza provocarea ca finalizata
            provocareUtilizator.Stare = "Finalizat";
            provocareUtilizator.DataFinal = DateTime.Today;

            var sarcina = _context.Sarcina.FirstOrDefault(s => s.Id == id);

            //creaza o noua inregistrare in SarcinaRealizata
            var sarcinaRealizata = new SarcinaRealizata
            {
                ProvocareUtilizatorId = provocareUtilizator.Id,
                SarcinaId = sarcina.Id,
                Data_Realizare = DateTime.Today,
                ZiuaRealizare = sarcina.Ziua
            };
            user.Streak++;

            _context.Update(user);
            _context.SarcinaRealizata.Add(sarcinaRealizata);
            _context.Update(provocareUtilizator);
            await _context.SaveChangesAsync();

            AcordaNouBadge(user);

            return RedirectToPage("./dashboard");
        }
        private bool SuntToateTaskurileFinalizate(ProvocareUtilizator provocareUtilizator)
        {
            //verifica daca toate taskurile din provocarea curenta au fost finalizate
            var tasksCompleted = _context.SarcinaRealizata
                .Where(sr => sr.ProvocareUtilizatorId == provocareUtilizator.Id)
                .Count();

            var totalTasks = _context.Sarcina
                .Where(s => s.ProvocareId == provocareUtilizator.ProvocareId)
                .Count();

            return tasksCompleted == totalTasks;
        }
        private void AcordaNouBadge(Utilizator user)
        {
            // Verificam daca utilizatorul a finalizat o provocare astazi
            var nextBadge = _context.Realizare.FirstOrDefault
                (b => !_context.RealizareUtilizator.Any
                (ru => ru.UtilizatorId == user.Id && ru.RealizareId == b.Id));

            if (nextBadge != null)
            {
                // Adaugam noul badge utilizatorului
                var newRealizareUtilizator = new RealizareUtilizator
                {
                    UtilizatorId = user.Id,
                    RealizareId = nextBadge.Id,
                    Data = DateTime.Today
                };
                _context.RealizareUtilizator.AddAsync(newRealizareUtilizator);
                _context.SaveChangesAsync();
            }
        }
        public bool IsTaskCompleted(int taskId)
        {
            var currentUser = User.Identity.Name;
            var user = _context.Utilizator.FirstOrDefault(u => u.Email == currentUser);
            var provocareUtilizator = _context.ProvocareUtilizator.FirstOrDefault(pu => pu.UtilizatorId == user.Id && pu.ProvocareId == Provocare.Id);

            var sarcina = _context.Sarcina.FirstOrDefault(s => s.Id == taskId);
            var currentDay = DateTime.Today.Day;

            if (taskId == 1)
            {
                return true;
            }

            else
            {
                return _context.SarcinaRealizata.Any(
                    sr => sr.SarcinaId == taskId &&
                    sr.ProvocareUtilizatorId == provocareUtilizator.Id)
                    && sarcina.Ziua <= currentDay;
            }
        }

    }
}