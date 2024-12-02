using System.ComponentModel.DataAnnotations;

namespace Challenges.WebApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [StringLength(30, MinimumLength = 3)]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$", ErrorMessage =
            "First name must start with a capital letter" +
            "(e.g., John or John Smith or John-Smith)")]
        [StringLength(30, MinimumLength = 3)]
        public string? LastName { get; set; }

        [RegularExpression("^0([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
        ErrorMessage = "Phone number must be in the format '0722-112-123' or '0722.122.123' or '0722 123 123'")]
        public string? PhoneNumber { get; set; }

        public int Streak { get; set; } = 0;
        public int Points { get; set; } = 0;
        //aceasta data este folosita pentru actualizarea streakului (se verifica daca 
        //data actualizarii streakului este data de azi. daca nu, in urma finalizarii
        //data actualizarii streakului este data de azi. daca nu, in urma finalizarii
        //unei sarcini streakul este incrementat insa daca in aceasi zi se finalizeaza
        //alte sarcini nu se va mai incrementa streakul.
        [DataType(DataType.Date)]
        public DateTime? LastStreakUpdateDate { get; set; }

		public ICollection<UserChallenge>? UserChallenges {  get; set; }
        public ICollection<UserBadge>? UserBadges {  get; set; }
        public ICollection<UserPreference>? UserCategories {  get; set; }
    }
}