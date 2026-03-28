using System.ComponentModel.DataAnnotations;

namespace Jubiler.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "Nazwa modelu")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string ModelName { get; set; }

        [Display(Name = "Cena")]
        [Range(0.01, 999999.99, ErrorMessage = "To pole jest wymagane")]
        public decimal Price { get; set; }

        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Kategoria")]
        public virtual Category? Category { get; set; }
        [Display(Name = "Materiał")]
        public virtual ICollection<Material>? Materials { get; set; } = new List<Material>();
    }
}
