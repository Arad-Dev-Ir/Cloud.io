namespace Cloudio.Core.Services.Caching;

using StackExchange.Redis;

public static partial class Extensions
{
    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services, Action<RedisServerConfigs> act)
    {
        var result = services.AddKeyedSingleton<ICache, RedisCache>(RedisCache.Key).Configure(act);

        RedisServerConfigs redisConfigs = new();
        act(redisConfigs);

        result.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var options = new ConfigurationOptions
            {
                EndPoints = { $"{redisConfigs.Host}:{redisConfigs.Port}" },
                Password = redisConfigs.Password
            };
            var connection = ConnectionMultiplexer.Connect(options);
            return connection;
        });

        return result;
    }

    public static IServiceCollection ConfigureInMemoryCache(this IServiceCollection services)
    => services.AddMemoryCache().AddKeyedTransient<ICache, MemoryCache>(MemoryCache.Key);

    public static IServiceCollection ConfigureFakeMemoryCache(this IServiceCollection services)
    => services.AddKeyedTransient<ICache, FakeCache>(FakeCache.Key);
}