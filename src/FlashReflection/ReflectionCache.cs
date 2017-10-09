using System;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace FlashReflection
{
    public sealed class ReflectionCache
    {
        private static volatile ReflectionCache _instance;
        private static object _syncRoot = new Object();
        private volatile MemoryCache _cachedTypes;
        private volatile MemoryCache _cachedProperties;
        private volatile MemoryCache _cachedMethods;
        private volatile MemoryCache _cachedTypeAttributes;
        private volatile MemoryCache _cachedPropertyAttributes;
        public static MemoryCacheOptions MemoryCacheOptions { get; set; }

        private ReflectionCache() { }

        public static ReflectionCache Instance
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        if (MemoryCacheOptions == null)
                            MemoryCacheOptions = new MemoryCacheOptions();
                        _instance = new ReflectionCache();
                        _instance._cachedTypes = new MemoryCache(MemoryCacheOptions);
                        _instance._cachedProperties = new MemoryCache(MemoryCacheOptions);
                        _instance._cachedMethods = new MemoryCache(MemoryCacheOptions);
                        _instance._cachedTypeAttributes = new MemoryCache(MemoryCacheOptions);
                        _instance._cachedPropertyAttributes = new MemoryCache(MemoryCacheOptions);
                    }
                }
                return _instance;
            }
        }

        public ReflectionType GetReflectionType<T>(MemoryCacheEntryOptions memoryCacheOptions = null) where T : class
        {
            return GetReflectionType(typeof(T), memoryCacheOptions);
        }

        public ReflectionType GetReflectionType(Type t, MemoryCacheEntryOptions memoryCacheOptions = null)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            var key = t.AssemblyQualifiedName;
            return _cachedTypes.GetOrCreate(key, entry =>
            {
                if (memoryCacheOptions != null)
                    entry.SetOptions(memoryCacheOptions);
                return new ReflectionType(t);
            });
        }
    }
}