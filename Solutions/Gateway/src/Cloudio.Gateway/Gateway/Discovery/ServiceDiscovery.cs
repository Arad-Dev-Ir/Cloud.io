namespace Cloudio.Gateway.Endpoint;

using Yarp.ReverseProxy.Configuration;
using Cloudio.Core.Services.Caching;
using Cloudio.Web.Services.Registry;

public class ServiceDiscovery([FromKeyedServices(RedisCache.Key)] ICache redisCache) : IProxyConfigProvider, IServiceDiscovery
{
    #region Constants

    private const string ServiceRegistryPattern = "Cloudio.Registry.*";
    private const string LoadBalancingPolicy = "RoundRobin";
    private const string Cluster = "Cluster";
    private const string Route = "Route";

    #endregion

    private ProxyConfig _proxyConfig = new(default!, default!);
    private readonly ICache _redisCache = redisCache;
    private IReadOnlyList<AppHost> _apps = [];

    public async Task DiscoverAsync()
    {
        _apps = await GetApps();
        SetConfig();
    }

    public IProxyConfig GetConfig()
    => _proxyConfig;

    #region Private Methods

    private async Task<List<AppHost>> GetApps()
    {
        var result = await _redisCache.GetAllAsync<AppHost>(ServiceRegistryPattern);
        return result;
    }

    private void SetConfig()
    {
        var previousConfig = _proxyConfig;
        _proxyConfig = new ProxyConfig(GetRoutes(), GetClusters());
        previousConfig?.OnChangedSignal();
    }

    private List<RouteConfig> GetRoutes()
    {
        var result = new List<RouteConfig>();

        foreach (var item in _apps)
        {
            result.Add(new RouteConfig
            {
                RouteId = $"{Route}-{item.Name}",
                ClusterId = $"{Cluster}-{item.Name}",
                Match = new() { Path = $"{item.UpstreamRoute}/{{**catch-all}}" },
                Transforms =
                [
                  new Dictionary<string, string>()
                  {
                      { "PathPattern", "{**catch-all}" }
                  }
                ]
            });
        }

        return result;
    }

    private List<ClusterConfig> GetClusters()
    {
        var result = new List<ClusterConfig>();

        foreach (var item in _apps)
        {
            var appName = item.Name;
            var cluster = new ClusterConfig()
            {
                ClusterId = $"{Cluster}-{appName}",
                LoadBalancingPolicy = LoadBalancingPolicy,
                Destinations = item.Instances
                .Select(e =>
               new
               {
                   Id = $"{e.Host}:{appName}:{e.Port}",
                   DestinationConfig = new DestinationConfig()
                   {
                       Address = $"{e.Scheme}://{e.Host}:{e.Port}/"
                   }
               }).ToDictionary(e => e.Id, e => e.DestinationConfig),
            };
            result.Add(cluster);
        }

        return result;
    }

    #endregion
}