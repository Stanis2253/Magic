
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MagicModels
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        [Required] 
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ShortDesc { get; set; }
        [Required]
        public bool IsActual { get; set; }

        public int ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
