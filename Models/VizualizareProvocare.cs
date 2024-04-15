using System.ComponentModel.DataAnnotations;

namespace Challenges.Models
{
	public class VizualizareProvocare
	{
		public int Id { get; set; }
		public int ProvocareId { get; set; }
		public Provocare Provocare { get; set; }
		public int UtilizatorId { get; set; }
		public Utilizator Utilizator { get; set; }
		[DataType(DataType.Date)]
		public DateTime DataVizualizare { get; set; }
	}
}
