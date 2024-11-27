using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Repository.Contracts;

namespace Talabat.Repositories.Basket_Repository
{
    public class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string? basketId)
        {
            basketId ??= string.Empty;

            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var status = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!status)
                return null;
            return await GetBasketAsync(basket.Id);

        }
    }
}
