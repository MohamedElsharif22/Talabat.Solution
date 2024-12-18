using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositories._Data
{
    public static class StoreDbContextSeed
    {

        public static async Task SeedAsync(StoreContext storeContext)
        {
            if (!storeContext.Brands.Any())
            {
                var brandsFile = await File.ReadAllTextAsync("../Talabat.Repository/_Data/seed/brands.json");
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
            if (!storeContext.Categories.Any())
            {
                var categoriesFile = await File.ReadAllTextAsync("../Talabat.Repository/_Data/seed/Categories.json");
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
            if (!storeContext.Products.Any())
            {
                var productsFile = await File.ReadAllTextAsync("../Talabat.Repository/_Data/seed/Products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsFile);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await storeContext.Products.AddAsync(product);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (!storeContext.DeliveryMethods.Any())
            {
                var deliveryDataFile = await File.ReadAllTextAsync("../Talabat.Repository/_Data/seed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryDataFile);
                if (deliveryMethods?.Count > 0)
                {
                    foreach (var method in deliveryMethods)
                    {
                        await storeContext.DeliveryMethods.AddAsync(method);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }


        }
    }
}
