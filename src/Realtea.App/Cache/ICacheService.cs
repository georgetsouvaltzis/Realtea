using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Realtea.App.Cache
{
    public interface ICacheService
    {
        string Get(string cacheKey);
        void Set(string cacheKey, object valueToStore);
    }

    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan TimeToLive = TimeSpan.FromMinutes(5);

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Set(string cacheKey, object valueToStore)
        {
            if (valueToStore == null)
                return;

            var serializedData = JsonSerializer.Serialize(valueToStore);

            _memoryCache.Set(cacheKey, serializedData, TimeToLive);
        }

        public void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

        public string Get(string cacheKey)
        {
            return _memoryCache.Get(cacheKey)?.ToString();
        }
    }
}
