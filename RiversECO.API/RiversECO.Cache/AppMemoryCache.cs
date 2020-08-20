using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RiversECO.Cache.Contracts;

namespace RiversECO.Cache
{
    public class AppMemoryCache : IAppMemoryCache
    {
        private readonly IMemoryCache _cache;

        public AppMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public TValue GetOrCreate<TValue>(string key, Func<TValue> cachedValueFactory)
        {
            if (string.IsNullOrWhiteSpace(key) || cachedValueFactory == null)
            {
                throw new ArgumentNullException();
            }

            RemoveNullableValueIfNeed(key);

            return _cache.GetOrCreate(key, entry =>
            {
                var cachedValue = cachedValueFactory.Invoke();
                return cachedValue;
            });
        }

        public async Task<TValue> GetOrCreateAsync<TValue>(string key, Func<Task<TValue>> cachedValueFactory)
        {
            if (string.IsNullOrWhiteSpace(key) || cachedValueFactory == null)
            {
                throw new ArgumentNullException();
            }

            RemoveNullableValueIfNeed(key);

            return await _cache.GetOrCreateAsync<TValue>(key, async entry =>
            {
                var cachedValue = await cachedValueFactory
                    .Invoke()
                    .ConfigureAwait(false);
                return cachedValue;
            });
        }

        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            _cache.Remove(key);
        }

        private void RemoveNullableValueIfNeed(string cacheKey)
        {
            if (_cache.Get(cacheKey) == null)
            {
                Remove(cacheKey);
            }
        }
    }
}
