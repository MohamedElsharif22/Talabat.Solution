using System.Text.Json;
using Talabat.Core.Entities;

namespace Talabat.Repositories.Generic_Repository.Data
{
    public static class StoreDbContextSeed
    {

        public static async Task SeedAsync(StoreContext storeContext)
        {
            if (storeContext.Brands.Count() == 0)
            {
                var brandsFile = await File.ReadAllTextAsync("../Talabat.Repository/Data/seed/brands.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(brandsFile);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await storeContext.Brands.AddAsync(brand);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (storeContext.Categories.Count() == 0)
            {
                var categoriesFile = await File.ReadAllTextAsync("../Talabat.Repository/Data/seed/Categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesFile);
                if (categories?.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        await storeContext.Categories.AddAsync(category);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (storeContext.Products.Count() == 0)
            {
                var productsFile = await File.ReadAllTextAsync("../Talabat.Repository/Data/seed/Products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsFile);
                if (products?.Count > 0)
                {
                    foreach (var category in products)
                    {
                        await storeContext.Products.AddAsync(category);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
        }
    }
}
