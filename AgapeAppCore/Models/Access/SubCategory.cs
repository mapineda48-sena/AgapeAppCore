using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgapeAppCore.Models.Access
{
    [Table("AccessSubCategory")]
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string FullName { get; set; }

        // Propiedad de navegación de la categoría padre
        public Category Category { get; set; }
    }

}