namespace Challenges.WebApp.Models
{
    public class Category
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public ICollection<ChallengeCategory>? ChallengeCategories { get; set; }

        public ICollection<UserPreference>? UserPreferences { get; set; }

    }
}
