using Microsoft.EntityFrameworkCore;

namespace AgapeAppCore.Models
{
    public partial class AppDbContext : DbContext
    {
        private readonly DbContextOptions<AppDbContext> options;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            this.options = options;
        }

        public AppDbContext Clone()
        {
            return new AppDbContext(this.options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnInventoryCreating(modelBuilder);
            OnAccessCreating(modelBuilder);
        }
    }
}
