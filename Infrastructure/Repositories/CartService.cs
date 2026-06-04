using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class CartService(IConnectionMultiplexer redis) : ICartService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task<bool> DeleteCartAsync(string id)
        {
           return await _database.KeyDeleteAsync(id);
        }

        public async Task<ShoppingCart?> GetCartAsync(string id)
        {
            var data = await _database.StringGetAsync(id);  
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(data.ToString());
        }

        public async Task<ShoppingCart?> SetCartAsync(ShoppingCart cart)
        {
            var data = JsonSerializer.Serialize(cart);
            var created= await _database.StringSetAsync(cart.Id, data, TimeSpan.FromDays(30));

            return created ? await GetCartAsync(cart.Id) : null;
        }
    }
}
