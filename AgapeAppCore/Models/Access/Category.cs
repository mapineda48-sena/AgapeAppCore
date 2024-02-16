using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AgapeAppCore.Models.Access
{
    [Table("AccessCategory")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string FullName { get; set; }

        // Propiedad de navegación para las subcategorías
        public List<SubCategory> SubCategories { get; set; }
    }

}