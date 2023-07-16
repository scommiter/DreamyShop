using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace DreamyShop.Repository.Helpers
{
    public class CacheHelper<TEntity>
    {
        private readonly IMemoryCache _cache;
        private ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public CacheHelper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TEntity> GetOrCreate(object key, Func<Task<TEntity>> createItem)
        {
            TEntity cacheEntry;

            if (!_cache.TryGetValue(key, out cacheEntry))// Look for cache key.
            {
                SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    if (!_cache.TryGetValue(key, out cacheEntry))
                    {
                        // Key not in cache, so get data.
                        cacheEntry = await createItem();
                        var cacheExpiryOptions = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                            Priority = CacheItemPriority.High,
                            SlidingExpiration = TimeSpan.FromSeconds(20)
                        };
                        _cache.Set(key, cacheEntry, cacheExpiryOptions);
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }
    }
}
