using System.ComponentModel.DataAnnotations;

namespace Challenges.WebApp.Models
{
    public class Badge
    {
        public int Id { get; set; } 
        public string Name {  get; set; }
        public string? Description { get; set; }
        public ICollection<UserBadge>? UserBadges { get; set; }
    }
}
