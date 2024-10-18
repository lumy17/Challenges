namespace Challenges.WebApp.Models
{
    public class CategorieProvocare
    {
        public int Id { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
        public int ProvocareId { get; set; }
        public Provocare Provocare { get; set; }
    }
}
