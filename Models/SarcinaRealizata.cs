using System.ComponentModel.DataAnnotations;

namespace Challenges.Models
{
    public class SarcinaRealizata
    {
        public int Id {  get; set; }    
        public int ProvocareUtilizatorId { get; set; }
        public ProvocareUtilizator  ProvocareUtilizator { get; set; }
        public int SarcinaId { get; set; }
        public Sarcina Sarcina { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Data_Realizare {  get; set; }   
        public int? ZiuaRealizare {  get; set; }   

    }
}
