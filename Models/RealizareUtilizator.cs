using System.ComponentModel.DataAnnotations;

namespace Challenges.Models
{
    public class RealizareUtilizator
    {
        public int Id {  get; set; }    
        public int UtilizatorId {  get; set; }
        public Utilizator Utilizator { get; set; }
        public int RealizareId {  get; set; }
        public Realizare Realizare { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
