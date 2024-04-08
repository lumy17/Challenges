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

        [Display(Name = "Nume Complet")]
        public string? NumeComplet
        {
            get
            {
                return Nume + " " + Prenume;
            }
        }
        public int Streak { get; set; } = 0;
        [DataType(DataType.Date)]
        public DateTime? DataUltimaActualizareStreak { get; set; }
        public ICollection<ProvocareUtilizator>? provocariUtilizatori {  get; set; }
        public ICollection<RealizareUtilizator>? realizariUtilizatori {  get; set; }
    }
}