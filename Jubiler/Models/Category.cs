using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Jubiler.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(50)]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
