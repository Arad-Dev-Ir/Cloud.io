namespace Cloudio.Core.Services.Caching;

using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Cloudio.Core.Services.Serialization;

public class MemoryCache(IMemoryCache cache, IJsonSerializer serializer) : ICache
{
    public const string Key = "MemoryCache";

    private readonly IMemoryCache _cache = cache;
    private readonly IJsonSerializer _serializer = serializer;

    public Task<T?> GetAsync<T>(string key) where T : class
    {
        _ = _cache.TryGetValue(key, out T? result);

        return Task.FromResult(result);
    }

    public Task SetAsync<T>(string key, T value) where T : class
    {
        var result = _serializer.Serialize(value);
        _cache.Set(key, result);

        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(string key)
    {
        _cache.Remove(key);

        var result = true;
        return Task.FromResult(result);
    }

    public Task DisposeAsync()
    {
        _cache.Dispose();

        return Task.CompletedTask;
    }
}