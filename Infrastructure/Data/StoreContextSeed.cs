using System.Reflection;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsyns(StoreContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText(path + @"/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products == null) return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }

        if (!context.DeliveryMethods.Any())
        {
            var dmData = File.ReadAllText(path + @"/Data/SeedData/delivery.json");
            var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

            if (methods == null) return;

            context.DeliveryMethods.AddRange(methods);

            await context.SaveChangesAsync();
        }
    }
}
