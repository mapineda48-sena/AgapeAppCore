using Microsoft.EntityFrameworkCore;
using AgapeAppCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AgapeApp.Infrastructure.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void InitializeDatabase(this WebApplication app)
        {
            var isDevelopment = app.Environment.IsDevelopment();

            using var scope = app.Services.CreateScope();          
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            EnsureCreated(appDbContext, isDevelopment);
        }

        private static void EnsureCreated(DbContext dbContext, bool isDevelopment = false)
        {
            if (isDevelopment)
            {
                dbContext.Database.EnsureDeleted();
            }

            // Asegura la creación de la base de datos si no existe
            dbContext.Database.EnsureCreated();
        }
    }
}
