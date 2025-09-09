using System;
using System.Collections.Concurrent;

namespace AsyncAwait;

public interface ICache<TKey, TValue>
{
    TValue Get(TKey key);
    void Set(TKey key, TValue value, TimeSpan? ttl = null);
    bool TryGet(TKey key, out TValue value);
    void Remove(TKey key);
    void Clear();
}


public class LockFreeCache<TKey, TValue>: ICache<TKey, TValue> where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, CacheItem<TValue>> _storage;
    private readonly TimeSpan _defaultTtl;
    private long _cleanupCounter = 0;

    public LockFreeCache(TimeSpan? defaultTtl = null)
    {
        _storage = new ConcurrentDictionary<TKey, CacheItem<TValue>>();
        _defaultTtl = defaultTtl ?? TimeSpan.FromHours(1);
    }
    
    public TValue Get(TKey key)
    {
        if (_storage.TryGetValue(key, out var value))
        {
            return value.Value;
        }
        throw new KeyNotFoundException($"Ключ '{key}' не найден в кэше.");
    }

    public void Set(TKey key, TValue value, TimeSpan? ttl = null)
    {
        throw new NotImplementedException();
    }

    public bool TryGet(TKey key, out TValue value)
    {
        throw new NotImplementedException();
    }

    public void Remove(TKey key)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }
}