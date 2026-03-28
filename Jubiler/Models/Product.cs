using System.ComponentModel.DataAnnotations;

namespace Jubiler.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
    }
}
