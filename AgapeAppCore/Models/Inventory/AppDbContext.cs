using Microsoft.EntityFrameworkCore;

namespace AgapeAppCore.Models
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Inventory.Product> Products { get; set; }

        public DbSet<Inventory.SubCategory> SubCategories { get; set; }

        public DbSet<Inventory.Category> Categories { get; set; }

        private void OnInventoryCreating(ModelBuilder modelBuilder)
        {
            // Configuración para Product
            modelBuilder.Entity<Inventory.Product>()
                .HasOne(p => p.Category) // Un producto pertenece a una categoría
                .WithMany() // No necesitamos especificar la propiedad de navegación inversa si no existe
                .HasForeignKey(p => p.CategoryId); // La clave foránea en Product para Category

            modelBuilder.Entity<Inventory.Product>()
                .HasOne(p => p.SubCategory) // Un producto pertenece a una subcategoría
                .WithMany() // No necesitamos especificar la propiedad de navegación inversa si no existe
                .HasForeignKey(p => p.SubCategoryId); // La clave foránea en Product para SubCategory

            modelBuilder.Entity<Inventory.Category>()
                .HasMany(c => c.SubCategories) // Una categoría tiene muchas subcategorías
                .WithOne(sc => sc.Category)   // Una subcategoría pertenece a una categoría
                .HasForeignKey(sc => sc.CategoryId); // La clave foránea en SubCategoria
        }
    }
}
