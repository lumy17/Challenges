using System.ComponentModel.DataAnnotations;

namespace Challenges.Models
{
    public class Realizare
    {
        public int Id { get; set; } 
        public string NumeRealizare {  get; set; }
        public string? Descriere { get; set; }
        public ICollection<RealizareUtilizator>? RealizariUtilizatori { get; set; }
    }
}
