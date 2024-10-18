using Challenges.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace Challenges.WebApp.Models
{
    public class RankingService
    {
        private readonly ApplicationDbContext _context;
        public RankingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Provocare> GetRankedChallenges(string userName)
        {
            var user = _context.Utilizator.FirstOrDefault
            (u => u.Email == userName);
            if (user == null)
            {
                // Handle the case where the user is not found
                return Enumerable.Empty<Provocare>(); // Return an empty collection or null as appropriate
            }
            var selectedCategories = _context.CategorieUtilizator
                .Where(cu => cu.UtilizatorId == user.Id)
                .Select(cu => cu.CategorieId)
                .ToList();
            //if (selectedCategories == null || !selectedCategories.Any())
            //{
            //    // Handle the case where no categories are selected
            //    return Enumerable.Empty<Provocare>(); // Return an empty collection or null as appropriate
            //}
            var allChallenges = _context.Provocare
                .Include(p => p.CategoriiProvocari)
                .ThenInclude(cp => cp.Categorie)
                        .Where(p => p.CategoriiProvocari.Any(cp => selectedCategories.Contains(cp.Categorie.Id)))
                .ToList();

            //ordoneaza provocarile in functie de nr de categ selectate de 
            //utilizator care sunt asociate fiecarei  provocari
            var rankedChallanges = allChallenges
                .OrderByDescending(p => p.CategoriiProvocari.Count
                (cp => selectedCategories.Contains(cp.Categorie.Id)))
                .ThenByDescending(p => p.CategoriiProvocari.Count);

            return rankedChallanges;
        }
    }
}
