using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreProductsSeed
    {
        public static async Task SeedAsync(StoreDbContext context)
        {
            if(!context.Products.Any())
            {
               var productsData=await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

                var Products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if(Products==null)
                {
                    return;
                }
                await context.Products.AddRangeAsync(Products);
                await context.SaveChangesAsync();
            }
        }
    }
}
