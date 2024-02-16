using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AgapeAppCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AgapeApp.Demo
{
    public static class DemoExtensions
    {
        public static void InitializeDemo(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            LoadCategroy(appDbContext);
        }

        public static void LoadCategroy(AppDbContext appDb)
        {
            var json = File.ReadAllText("Demo/Category.json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json) ?? throw new Exception("Fail category demo");

            foreach (var item in data)
            {
                var SubCategories = new List<AgapeAppCore.Models.Inventory.SubCategory>();

                foreach (var sub in item.Value)
                {
                    SubCategories.Add(new AgapeAppCore.Models.Inventory.SubCategory()
                    {
                        FullName = sub
                    });
                }

                var category = appDb.Categories.Add(new AgapeAppCore.Models.Inventory.Category()
                {
                    FullName = item.Key,
                    SubCategories = SubCategories
                });

                appDb.SaveChanges();

                // Console.WriteLine($"category: {item.Key}");
                // foreach (var sub in item.Value)
                // {
                //     Console.WriteLine($"subcategory: {sub}");
                // }
            }

        }
    }
}