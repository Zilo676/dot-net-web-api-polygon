namespace AsyncAwait;

public record CacheItem<TValue>(
    TValue Value,
    long? ExpirationTicks
);