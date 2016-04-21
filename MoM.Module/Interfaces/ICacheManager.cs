using Microsoft.Extensions.Caching.Memory;
using System;

namespace MoM.Module.Interfaces
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Set(string key, object data, TimeSpan timeout, CacheItemPriority priority);
        void Remove(string key);
    }
}
