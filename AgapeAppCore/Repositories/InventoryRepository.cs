using AgapeAppCore.Models;
using AgapeAppCore.Infrastructure.Cache;
using AgapeAppCore.Infrastructure.Storage;
using AgapeAppCore.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace AgapeApp.Repositories
{
    public class InventoryRepository(AppDbContext db, ICacheManager cache, IStorageManager storage)
    {
        private readonly AppDbContext db = db;
        private readonly ICacheManager cache = cache;
        private readonly IStorageManager storage = storage;

        public Task<List<Product>> GetProducts()
        {
            return db.Products.ToListAsync();
        }

        public async Task SaveProduct(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        public Task<List<Category>> GetCategories()
        {
            return cache.GetAsync("Inventory.Categories", async () =>
            {
                var clone = db.Clone();

                var res = await clone.Categories.ToListAsync();

                await clone.DisposeAsync();

                return res;
            });
        }

        public Task<List<SubCategory>> GetSubCategories(int CategoryId)
        {
            return cache.GetAsync("Inventory.SubCategories", async () =>
            {
                var clone = db.Clone();

                var res = await clone.SubCategories.Where(r => r.CategoryId == CategoryId).ToListAsync();

                await clone.DisposeAsync();

                return res;
            });
        }

    }
}