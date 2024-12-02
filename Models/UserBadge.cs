using System.ComponentModel.DataAnnotations;

namespace Challenges.WebApp.Models
{
    public class UserBadge
    {
        public int Id {  get; set; }    
        public int UserId {  get; set; }
        public AppUser User { get; set; }
        public int BadgeId {  get; set; }
        public Badge Badge { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
