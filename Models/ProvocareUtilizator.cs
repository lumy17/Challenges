using System.ComponentModel.DataAnnotations;

namespace Challenges.WebApp.Models
{
    public class ProvocareUtilizator
    {
        public int Id { get; set; }
        public int ProvocareId { get; set; }
        public Provocare Provocare { get; set; }
        public int UtilizatorId { get; set; }
        public Utilizator Utilizator { get; set; }

        [DataType(DataType.Date)] //attribute(adnotare) utilizat pentru a specifica tipul de date al unui camp in cadrul unei clase
        public DateTime? Data_start { get; set; }
        [DataType(DataType.Date)] //avem nevoie doar de partea de data, fara informatii despre ora
        public DateTime? DataFinal { get; set; }
        public string Stare { get; set; } = "in desfasurare";
        public int? ZiuaCurenta { get; set; }
    }
}
