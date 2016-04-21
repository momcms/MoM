using System;
using Microsoft.Extensions.Caching.Memory;

namespace MoM.Module.Managers
{
    public class CacheManager
    {
        private IMemoryCache MemoryCache;

        public CacheManager(MemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            return (T)MemoryCache.Get(key);
        }

        public void Set(string key, object data, TimeSpan timeout, CacheItemPriority priority)
        {
            MemoryCache.Set(key, data, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = timeout, Priority = priority });
        }

        public void Remove(string key)
        {
            MemoryCache.Remove(key);
        }
    }
}
