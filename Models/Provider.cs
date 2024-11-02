using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketWEB.Models
{
    [Table("Providers")]
    public class Providers
    {
        [Key]
        public int ProviderID { get; set; }
        public string name { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
