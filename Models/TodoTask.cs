namespace Challenges.WebApp.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        public int Day { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int? ChallengeId { get; set; }

        public Challenge? Challenge { get; set; }

        public ICollection<FinishedTask>? FinishedTask { get; set; }
    }
}
