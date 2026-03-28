using System.ComponentModel.DataAnnotations;

namespace Jubiler.Models
{
    public class Material
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
