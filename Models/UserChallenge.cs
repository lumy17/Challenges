using System.ComponentModel.DataAnnotations;

namespace Challenges.WebApp.Models
{
    public class UserChallenge
    {
        public int Id { get; set; }

        public int ChallengeId { get; set; }

        public Challenge Challenge { get; set; }

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string CurrentState { get; set; } = "ongoing";

        public int? CurrentDay { get; set; }
    }
}
