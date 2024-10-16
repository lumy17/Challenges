namespace Challenges.Models
{
    public class Provocare
    { 
        public int Id { get; set; } 
        public string Nume { get; set; }
        public string Descriere {  get; set; }  
        public string Categorie { get; set; }
        public string UrlImagine { get; set; }
        public int Durata { get; set; }
        public int Vizualizari { get; set; } = 0;
        public ICollection<Sarcina>? Sarcini { get; set; }
        public ICollection<ProvocareUtilizator>? provocariUtilizatori { get; set; }
        public ICollection<CategorieProvocare> CategoriiProvocari { get; set; }
   
        
    }
}
