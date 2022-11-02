using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory iloggerFactory)
        {
            try
            {
                if (!context.productBrands.Any())
                {
                    var brandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                
                foreach (var item in brands)
                {
                    context.productBrands.Add(item);
                }

                await context.SaveChangesAsync();
                }

                 if (!context.productTypes.Any())
                {
                    var prodTypeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var productsTypes = JsonSerializer.Deserialize<List<ProductType>>(prodTypeData);
                
                foreach (var item in productsTypes)
                {
                    context.productTypes.Add(item);
                }

                await context.SaveChangesAsync();
                }
                
                 if (!context.Products.Any())
                {
                    var ProdData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProdData);
                
                foreach (var item in products)
                {
                    context.Products.Add(item);
                }

                await context.SaveChangesAsync();
                }
            }
            catch (System.Exception ex )
            {
                var logger = iloggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}