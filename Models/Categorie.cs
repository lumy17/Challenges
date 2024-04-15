namespace Challenges.Models
{
    public class Categorie
    {
        public int Id { get; set; } 
        public string Nume { get; set; }
        public ICollection<CategorieProvocare>? CategoriiProvocari { get; set; }
        public ICollection<CategorieUtilizator>? CategoriiUtilizatori { get; set; }

    }
}
