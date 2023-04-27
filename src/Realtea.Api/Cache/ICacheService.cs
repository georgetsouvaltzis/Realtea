using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Realtea.Infrastructure.Settings;
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
        private readonly CacheSettings _settings;

        public CacheService(IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings)
        {
            _memoryCache = memoryCache;
            _settings = cacheSettings.Value;
        }

        public void Set(string cacheKey, object valueToStore)
        {
            if (valueToStore == null)
                return;

            var serializedData = JsonSerializer.Serialize(valueToStore);

            _memoryCache.Set(cacheKey, serializedData, TimeSpan.FromMinutes(_settings.CacheTimeToLiveInMinutes));
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
