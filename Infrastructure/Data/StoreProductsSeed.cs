using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreProductsSeed
    {
        public static async Task SeedAsync(StoreDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

                var Products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (Products == null)
                {
                    return;
                }
                await context.Products.AddRangeAsync(Products);
                await context.SaveChangesAsync();
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Customer"))
                await roleManager.CreateAsync(new IdentityRole("Customer"));

            if (await userManager.FindByEmailAsync("admin@shop.com") == null)
            {
                var adminUser = new AppUser
                {
                    FirstName = "Waleed",
                    LastName = "Shuaib",
                    Email = "admin@shop.com",
                    UserName = "admin@shop.com"
                };

                var result = await userManager.CreateAsync(adminUser, "Password123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
