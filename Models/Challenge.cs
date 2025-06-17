namespace Challenges.WebApp.Models
{
    public class Challenge
    { 
        public int Id { get; set; } 

        public string Name { get; set; }

        public string Description {  get; set; }

        public string ImageUrl { get; set; }

        public int Duration { get; set; }

        public int Views { get; set; } = 0;

        public ICollection<TodoTask>? TodoTasks { get; set; }

        public ICollection<UserChallenge>? UserChallenges { get; set; }

        public ICollection<ChallengeCategory> ChallengeCategories { get; set; }
    }
}
