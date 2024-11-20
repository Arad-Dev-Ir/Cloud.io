namespace Cloudio.Web.Services.Registry;

using Microsoft.Extensions.Options;
using Cloudio.Core.Services.Caching;

public class ServiceRegistry([FromKeyedServices(RedisCache.Key)] ICache cache, IOptionsMonitor<AppRegistryConfigs> options) : IServiceRegistry
{
    private readonly ICache _cache = cache;
    private readonly AppRegistryConfigs _appRegistryConfigs = options.CurrentValue;

    public async Task Register(Uri uri)
    {
        var key = _appRegistryConfigs.Key;

        var scheme = uri.Scheme;
        var host = uri.Host;
        var port = uri.Port;

        var data = await _cache.GetAsync<AppHost>(key);
        if (data is { })
        {
            var exists = data.Instances.Exists(e => e.Scheme == scheme && e.Host == host && e.Port == port);
            if (!exists)
            {
                data.Instances.Add(new Instance { Scheme = scheme, Host = host, Port = port });
                await _cache.SetAsync(key, data);
            }
        }
        else
        {
            data = new AppHost
            {
                Name = _appRegistryConfigs.Name,
                UpstreamRoute = _appRegistryConfigs.UpstreamRoute,
                Instances = [new Instance { Scheme = scheme, Host = host, Port = port }]
            };
            await _cache.SetAsync(key, data);
        }
    }

    public async Task Deregister(Uri uri)
    {
        var key = _appRegistryConfigs.Key;

        var data = await _cache.GetAsync<AppHost>(key);
        if (data is { })
        {
            var instance = data.Instances
            .Where(e => e.Scheme == uri.Scheme && e.Host == uri.Host && e.Port == uri.Port)
            .FirstOrDefault();

            if (instance is { })
            {
                data.Instances.Remove(instance);
                await _cache.SetAsync(key, data);
            }
            if (data.Instances.Count == 0)
                await _cache.DeleteAsync(key);
        }
        await _cache.DisposeAsync();
    }
}