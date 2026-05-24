using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class ProductRepository(StoreDbContext storeDbContext) : IProductRepository
    {
        public void AddProductAsync(Product product)
        {
            storeDbContext.Products.Add(product);
        }

        public void DeleteProductAsync(Product product)
        {
            storeDbContext.Products.Remove(product);
        }

        public async Task<IEnumerable<string>> GetProductBrandsAsync()
        {
            return await storeDbContext.Products.Select(p => p.Brand).Distinct().ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await storeDbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string? brand, string? type, string? sort)
        {
            var query = storeDbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(p => p.Brand == brand);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(p => p.Type == type);
            }
            
                switch (sort)
                {
                    case "priceAsc":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                   
                    default:
                    query = query.OrderBy(p => p.Name);
                    break;
                }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetProductTypesAsync()
        {
            return await storeDbContext.Products.Select(p => p.Type).Distinct().ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return storeDbContext.Products.Any(e => e.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await storeDbContext.SaveChangesAsync() > 0;
        }

        public void UpdateProductAsync(Product product)
        {
           storeDbContext.Entry(product).State = EntityState.Modified;
        }
    }
}
