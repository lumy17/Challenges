﻿using Challenges.WebApp.Data;
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
        public IEnumerable<Challenge> GetRankedChallenges(string userName)
        {
            var user = _context.AppUser.FirstOrDefault
            (u => u.Email == userName);
            if (user == null)
            {
                // Handle the case where the user is not found
                return Enumerable.Empty<Challenge>(); // Return an empty collection or null as appropriate
            }
            var selectedCategories = _context.UserPreference
                .Where(cu => cu.AppUserId == user.Id)
                .Select(cu => cu.CategoryId)
                .ToList();
            //if (selectedCategories == null || !selectedCategories.Any())
            //{
            //    // Handle the case where no categories are selected
            //    return Enumerable.Empty<Provocare>(); // Return an empty collection or null as appropriate
            //}
            var allChallenges = _context.Challenge
                .Include(p => p.ChallengeCategories)
                .ThenInclude(cp => cp.Category)
                        .Where(p => p.ChallengeCategories.Any(cp => selectedCategories.Contains(cp.Category.Id)))
                .ToList();

            //ordoneaza provocarile in functie de nr de categ selectate de 
            //utilizator care sunt asociate fiecarei  provocari
            var rankedChallanges = allChallenges
                .OrderByDescending(p => p.ChallengeCategories.Count
                (cp => selectedCategories.Contains(cp.Category.Id)))
                .ThenByDescending(p => p.ChallengeCategories.Count);

            return rankedChallanges;
        }
    }
}