namespace Challenges.Models
{
    public class CategorieUtilizator
    {
        public int Id { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
        public int UtilizatorId { get; set; }
        public Utilizator Utilizator { get; set; }
    }
}
