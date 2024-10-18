namespace Challenges.WebApp.Models
{
    public class Sarcina
    {
        public int Id { get; set; }
        public int Ziua { get; set; }   
        public string Nume { get; set; }
        public string? Descriere { get; set; }
        public int ProvocareId { get; set; }
        public Provocare Provocare { get; set; }
        public ICollection<SarcinaRealizata>? SarciniRealizate { get; set; }
    }
}
