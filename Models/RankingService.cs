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
        public IEnumerable<Challenge> GetRankedChallenges(string email)
        {
            var user = _context.AppUser.FirstOrDefault(u => u.Email == email);

            var selectedCategories = _context.UserPreference
                .Where(cu => cu.AppUserId == user.Id)
                .Select(cu => cu.CategoryId)
                .ToList();

            if (selectedCategories == null)
            {
                return Enumerable.Empty<Challenge>();
            }

            var allChallenges = _context.Challenge
                .Include(p => p.ChallengeCategories)
                .ThenInclude(cp => cp.Category)
                        .Where(p => p.ChallengeCategories.Any(cp => selectedCategories.Contains(cp.Category.Id)))
                .ToList();

            var rankedChallanges = allChallenges
                .OrderByDescending(p => p.ChallengeCategories.Count(cp => selectedCategories.Contains(cp.Category.Id)))
                .ThenByDescending(p => p.ChallengeCategories.Count);

            return rankedChallanges;
        }
    }
}
