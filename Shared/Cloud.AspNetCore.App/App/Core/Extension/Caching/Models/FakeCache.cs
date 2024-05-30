namespace Cloud.Core.Extensions.Caching;

using Microsoft.Extensions.Logging;
using Serialization;

public class FakeCache : ICache
{
    public FakeCache(ILogger<FakeCache> logger, IJsonSerializer serializer)
    {
        _logger = logger;
        Initialize();
        _serializer = serializer;
    }

    private readonly ILogger<FakeCache> _logger;
    private readonly IJsonSerializer _serializer;

    private void Initialize()
    => _logger.LogInformation("In-Memory Cache started to work...");

    public void Add<T>(string key, T value, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
    {
        var message = "{Value} is cached with key: {Key}, data: {Data}, absolute expiration: {Absolute} and sliding expiration: {Sliding}.";
        var serializedValue = _serializer.Serialize(value);
        var absolute = absoluteExpiration.ToString();
        var sliding = slidingExpiration.ToString();
        _logger.LogTrace(message, typeof(T), key, serializedValue, absolute, sliding);
    }

    public T Get<T>(string key)
    {
        var result = default(T);
        _logger.LogTrace("In-Memory Cache started to try getting cache with key: {Key}", key);
        return result;
    }

    public void Remove(string key)
    => _logger.LogTrace("In-Memory Cache removed cache with key: {Key}", key);
}