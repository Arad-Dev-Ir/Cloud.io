namespace Cloud.Core.Extensions.Caching;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Serialization;

public class MemoryCache : ICache
{
    public MemoryCache(IMemoryCache cache, IJsonSerializer serializer, ILogger<MemoryCache> logger)
    {
        _cache = cache;
        _serializer = serializer;
        _logger = logger;
        Initialize();
    }

    private readonly IMemoryCache _cache;
    private readonly IJsonSerializer _serializer;
    private readonly ILogger<MemoryCache> _logger;

    private void Initialize()
    => _logger.LogInformation("In-Memory Cache started to work...");

    public void Add<T>(string key, T value, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
    {
        var message = "{Value} is cached with key: {Key}, data: {Data}, absolute expiration: {Absolute} and sliding expiration: {Sliding}.";
        var serializedValue = _serializer.Serialize(value);
        var absolute = absoluteExpiration.ToString();
        var sliding = slidingExpiration.ToString();
        _logger.LogTrace(message, typeof(T), key, serializedValue, absolute, sliding);
        _cache.Set(key, value);
    }

    public T Get<T>(string key)
    {
        _logger.LogTrace("In-Memory Cache started to try getting cache with key: {Key}", key);
        var isSucceeded = _cache.TryGetValue(key, out T? result);
        if (isSucceeded)
        {
            var serializedValue = _serializer.Serialize(result);
            _logger.LogTrace("In-Memory Cache got the cache with key: {Key} and data: {Data}", key, serializedValue);
        }
        else
            _logger.LogTrace("In-Memory Cache was failed to get cache with key: {Key}", key);
        return result;
    }

    public void Remove(string key)
    {
        _logger.LogTrace("In-Memory Cache removed cache with key: {Key}", key);
        _cache.Remove(key);
    }
}
