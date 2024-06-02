namespace APIGateway.Endpoint.APIs;

using Yarp.ReverseProxy.Configuration;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;

public class ServiceDiscoveryProxy : BackgroundService, IProxyConfigProvider
{
    private ProxyConfig _proxyConfig;
    private List<RouteConfig> _routes;
    private readonly DiscoveryClient _discoveryClient;

    public ServiceDiscoveryProxy(IDiscoveryClient discoveryClient)
    {
        _discoveryClient = (DiscoveryClient)discoveryClient;
        Initialize();
    }

    private void Initialize()
    {
        _routes = GetRoutes();
        GetClusters();
    }


    public IProxyConfig GetConfig()
    => _proxyConfig;

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            GetClusters();
            await Task.Delay(30000, token);
        }
    }

    #region Methods

    private static List<RouteConfig> GetRoutes()
    {
        var result = new List<RouteConfig>()
        {
            new RouteConfig()
            {
               RouteId = "Route-BasicInformation",
               ClusterId = "Cluster-BasicInformation",
               Match = new() { Path = "km/{**catch-all}" },
               Transforms = new List<Dictionary<string, string>>()
                {
                  new()
                  {
                      { "PathPattern", "{**catch-all}" }
                  }
                }
            },
            new RouteConfig()
            {
               RouteId = "Route-NewsManagement",
               ClusterId = "Cluster-NewsManagement",
               Match = new() { Path = "nm/{**catch-all}" },
               Transforms = new List<Dictionary<string, string>>()
                {
                  new()
                  {
                      { "PathPattern", "{**catch-all}" }
                  }
                }
            }
        };

        return result;
    }

    private void GetClusters()
    {
        var apps = _discoveryClient.Applications.GetRegisteredApplications();
        var result = new List<ClusterConfig>();
        foreach (var item in apps)
        {
            var cluster = new ClusterConfig()
            {
                ClusterId = $"Cluster-{item.Name}",
                LoadBalancingPolicy = "RoundRobin",
                Destinations = item.Instances
                .Select(e =>
               new
               {
                   Id = e.InstanceId,
                   DestinationConfig = new DestinationConfig()
                   {
                       Address = $"https://{e.HostName}:{e.Port}/"
                   }
               }).ToDictionary(e => e.Id, e => e.DestinationConfig),
            };
            result.Add(cluster);
        }

        var previousConfig = _proxyConfig;
        _proxyConfig = new ProxyConfig(_routes, result);
        previousConfig?.OnChangeSignal();
    }

    #endregion
}