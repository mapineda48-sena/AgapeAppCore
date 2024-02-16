using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgapeAppCore.Models.Access
{
    [Table("AccessProduct")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; } // Clave primaria

        [Required]
        public string FullName { get; set; } // Propiedad para el nombre del producto

        [Required]
        public string Description { get; set; } // Propiedad para la descripción del producto

        [Required]
        public string ImageUrl { get; set; } // Propiedad para una url que lleva a una imagen del producto

        // Claves foráneas
        [Required]
        public int CategoryId { get; set; } // Clave foránea para Category

        [Required]
        public int SubCategoryId { get; set; } // Clave foránea para SubCategory

        // Propiedades de navegación
        public Category Category { get; set; } // Categoría a la que pertenece el producto

        public SubCategory SubCategory { get; set; } // Subcategoría a la que pertenece el producto
    }
}