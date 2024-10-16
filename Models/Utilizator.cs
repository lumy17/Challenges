using System.ComponentModel.DataAnnotations;

namespace Challenges.Models
{
    public class Utilizator
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [StringLength(30, MinimumLength = 3)]
        public string? Nume { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$",ErrorMessage = 
            "Prenumele trebuie sa inceapa cu majuscula" + 
            "(ex.Ana sau Ana Maria sau Ana-Maria")]
        [StringLength(30, MinimumLength = 3)]
        public string? Prenume { get; set; }

        [RegularExpression("^0([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
        ErrorMessage = "Telefonul trebuie sa fie de forma'0722-112-123' sau '0722.122.123' sau '0722 123 123'")]
        public string? NumarTelefon { get; set; }

        public int Streak { get; set; } = 0;
        public int Puncte { get; set; } = 0;
        //aceasta data este folosita pentru actualizarea streakului (se verifica daca 
        //data actualizarii streakului este data de azi. daca nu, in urma finalizarii
        //data actualizarii streakului este data de azi. daca nu, in urma finalizarii
        //unei sarcini streakul este incrementat insa daca in aceasi zi se finalizeaza
        //alte sarcini nu se va mai incrementa streakul.
        [DataType(DataType.Date)]
        public DateTime? DataUltimaActualizareStreak { get; set; }

		public ICollection<ProvocareUtilizator>? provocariUtilizatori {  get; set; }
        public ICollection<RealizareUtilizator>? realizariUtilizatori {  get; set; }
        public ICollection<CategorieUtilizator>? CategoriiUtilizatori {  get; set; }
    }
}