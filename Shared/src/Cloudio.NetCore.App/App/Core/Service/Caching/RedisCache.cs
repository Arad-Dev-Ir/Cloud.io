namespace Cloudio.Core.Services.Caching;

using Cloudio.Core.Services.Serialization;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

public class RedisCache : ICache
{
    public const string Key = "RedisCache";

    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _database;
    private readonly IJsonSerializer _serializer;
    private readonly RedisServerConfigs _cacheConfigs;

    public RedisCache(IConnectionMultiplexer redis, IJsonSerializer serializer, IOptionsMonitor<RedisServerConfigs> options)
    {
        _redis = redis;
        _database = _redis.GetDatabase();
        _cacheConfigs = options.CurrentValue;
        _serializer = serializer;
    }

    public async Task<TOutput?> GetAsync<TOutput>(string key) where TOutput : class
    {
        var data = await _database.StringGetAsync(new RedisKey(key));

        var result = data.HasValue ? _serializer.Deserialize<TOutput>(data!) : default;
        return result;
    }

    public async Task<List<TOutput>> GetAllAsync<TOutput>(string? pattern = "*") where TOutput : class
    {
        var result = new List<TOutput>();

        var keys = _redis.GetServer(_cacheConfigs.Host, _cacheConfigs.Port)
        .Keys(pattern: pattern)
        .AsQueryable()
        .Select(e => e.ToString())
        .ToList();

        foreach (var item in keys)
        {
            var data = await GetAsync<TOutput>(item);
            if (data is { })
                result.Add(data);
        }

        return await Task.FromResult(result);
    }

    public async Task SetAsync<TInput>(string key, TInput value) where TInput : class
    {
        var data = _serializer.Serialize<TInput>(value);

        await _database.StringSetAsync(key, data);
    }

    public async Task<bool> DeleteAsync(string key)
    {
        var result = await _database.KeyDeleteAsync(key);
        return result;
    }

    public async Task DisposeAsync()
    {
        await _redis.CloseAsync();
        await _redis.DisposeAsync();
    }
}