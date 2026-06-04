using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ICartService
    {
        Task<bool> DeleteCartAsync(string key);
        Task<ShoppingCart?> GetCartAsync(string key);
        Task<ShoppingCart?> SetCartAsync(ShoppingCart cart);

    }
}
