using Microsoft.EntityFrameworkCore;
using AgapeApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AgapeAppCore.Infrastructure.Storage;
using AgapeAppCore.Infrastructure.Cache;
using AgapeAppCore.Models;


namespace AgapeApp.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<InventoryRepository>();
        }

        public static void AddStorageService(this IServiceCollection services, string? connectionString)
        {
            services.AddSingleton<IStorageManager>(new MinioService(connectionString ?? string.Empty));
        }

        public static void AddCacheService(this IServiceCollection services, string? connectionString)
        {
            services.AddSingleton<ICacheManager>(new RedisClient(connectionString ?? string.Empty));
        }

        public static void AddAppDbContext(this IServiceCollection services, string? connectionString)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

            // AddDbContextPool
            //services.AddDbContextPool<AppDbContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
