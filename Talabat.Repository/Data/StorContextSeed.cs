using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Repository.Data
{
    public class StorContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext)
        {
            if(!storeContext.ProductBrands.Any())
            {
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                foreach (var brand in Brands)
                {
                    await storeContext.Set<ProductBrand>().AddAsync(brand);
                }
                await storeContext.SaveChangesAsync();

            }

            if (!storeContext.ProductTypes.Any())
            {
                var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                foreach (var Type in Types)
                {
                    await storeContext.Set<ProductType>().AddAsync(Type);
                }
                await storeContext.SaveChangesAsync();
            }

            if (!storeContext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                foreach (var Product in Products)
                {
                   await storeContext.Set<Product>().AddAsync(Product);
                }
                var x = await storeContext.SaveChangesAsync();
            }

            //if (!storeContext.Products.Any())
            //{
            //    var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
            //    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

            //    foreach (var Product in Products)
            //    {
            //        await storeContext.Set<Product>().AddAsync(Product);
            //    }
            //    var x = await storeContext.SaveChangesAsync();
            //}


        }

    }
}
