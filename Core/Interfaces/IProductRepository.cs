using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(string? brand, string? type, string? sort);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<string>> GetProductBrandsAsync();
        Task<IEnumerable<string>> GetProductTypesAsync();
        void UpdateProductAsync(Product product);
        void AddProductAsync(Product product);
        void DeleteProductAsync(Product product);
        bool ProductExists(int id);
        Task<bool> SaveChangesAsync();
    }
}
