namespace Talabat.Core.Services.Contracts
{
    public interface IResponseCacheService
    {
        Task CachResponseAsync(string key, object response, TimeSpan timetoLive);

        Task<string?> GetCachedResponse(string key);
    }
}
