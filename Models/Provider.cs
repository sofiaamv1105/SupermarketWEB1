using System.ComponentModel.DataAnnotations;

namespace SupermarketWEB.Models
{
	public class Provider
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public DateTime Birthday { get; set; }
		public string Email { get; set; }
		public ICollection<Product> Products { get; set; } = default!;
	}
}
