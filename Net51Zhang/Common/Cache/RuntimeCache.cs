using System;
using System.Runtime.Caching;

namespace Net51Zhang.Common.Cache
{
    public class RuntimeCache : ICache
    {
        private readonly MemoryCache _cache;
        private readonly CacheItemPolicy _defaultCacheItemPolicy;

        public RuntimeCache()
        {
            _cache = MemoryCache.Default;
            _defaultCacheItemPolicy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromSeconds(60 * 2) };
        }

        public void Add(string key, object obj)
        {
            var cacheItem = new CacheItem(key, obj);
            _cache.Set(cacheItem, _defaultCacheItemPolicy);
        }

        public void Add(string key, object obj, int seconds)
        {
            _cache.Set(key, obj, DateTimeOffset.Now.AddSeconds(seconds));
        }

        public void Add(string key, object obj, TimeSpan slidingExpiration)
        {
            var cacheItem = new CacheItem(key, obj);
            var cacheItemPolicy = new CacheItemPolicy { SlidingExpiration = slidingExpiration };
            _cache.Set(cacheItem, cacheItemPolicy);
        }

        public bool Exists(string key)
        {
            return _cache.Get(key) != null;
        }

        public T Get<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        public T GetOrAdd<T>(string key, Func<T> factoryFunc, int second)
        {
            if (this.Exists(key))
                return this.Get<T>(key);
            else
            {
                var obj = factoryFunc();
                this.Add(key, obj, second);
                return (T)obj;
            }
        }

        public T GetOrAdd<T>(string key, Func<T> factoryFunc, TimeSpan slidingExpiration)
        {
            if (this.Exists(key))
                return this.Get<T>(key);
            else
            {
                var obj = factoryFunc();
                this.Add(key, obj, slidingExpiration);
                return (T)obj;
            }
        }

        public void Max(string key, object obj)
        {
            var cacheItem = new CacheItem(key, obj);
            var cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTime.MaxValue.AddYears(-1), Priority = CacheItemPriority.NotRemovable };
            _cache.Set(cacheItem, cacheItemPolicy);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public bool Test()
        {
            const string key = "_##**Test**##_";
            const string obj = "Test";
            Add(key, obj);
            var result = Get<string>(key);
            return result == obj;
        }
    }
}