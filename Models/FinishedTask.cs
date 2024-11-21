using System.ComponentModel.DataAnnotations;

namespace Challenges.WebApp.Models
{
    public class FinishedTask
    {
        public int Id {  get; set; }    
        public int UserChallengeId { get; set; }
        public UserChallenge UserChallenge { get; set; }
        public int TodoTaskId { get; set; }
        public TodoTask TodoTask { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletionDate {  get; set; }   
        public int? CompletionDay {  get; set; }   

    }
}
