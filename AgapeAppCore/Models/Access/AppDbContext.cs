using Microsoft.EntityFrameworkCore;

namespace AgapeAppCore.Models
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Access.Product> aProducts { get; set; }

        public DbSet<Access.SubCategory> aSubCategories { get; set; }

        public DbSet<Access.Category> aCategories { get; set; }

        private void OnAccessCreating(ModelBuilder modelBuilder)
        {
            // Configuración para Product
            modelBuilder.Entity<Access.Product>()
                .HasOne(p => p.Category) // Un producto pertenece a una categoría
                .WithMany() // No necesitamos especificar la propiedad de navegación inversa si no existe
                .HasForeignKey(p => p.CategoryId); // La clave foránea en Product para Category

            modelBuilder.Entity<Access.Product>()
                .HasOne(p => p.SubCategory) // Un producto pertenece a una subcategoría
                .WithMany() // No necesitamos especificar la propiedad de navegación inversa si no existe
                .HasForeignKey(p => p.SubCategoryId); // La clave foránea en Product para SubCategory

            modelBuilder.Entity<Access.Category>()
                .HasMany(c => c.SubCategories) // Una categoría tiene muchas subcategorías
                .WithOne(sc => sc.Category)   // Una subcategoría pertenece a una categoría
                .HasForeignKey(sc => sc.CategoryId); // La clave foránea en SubCategoria
        }
    }
}
