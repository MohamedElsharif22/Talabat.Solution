using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Services.Contracts;

namespace Talabat.Services.Caching_Service
{
    public class ResponseCacheService(IConnectionMultiplexer redis) : IResponseCacheService
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task CachResponseAsync(string key, object response, TimeSpan timetoLive)
        {
            if (response is null) return;

            var serializedResponse = JsonSerializer.Serialize(response, options: new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            await _database.StringSetAsync(key, serializedResponse, timetoLive);
        }

        public async Task<string?> GetCachedResponse(string key)
        {
            var response = await _database.StringGetAsync(key);

            if (response.IsNullOrEmpty) return null;

            return response;
        }
    }
}
