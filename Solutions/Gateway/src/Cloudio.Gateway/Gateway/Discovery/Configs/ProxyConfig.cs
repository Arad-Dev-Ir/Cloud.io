namespace Cloudio.Gateway.Endpoint;

using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

public class ProxyConfig : IProxyConfig
{
    private readonly CancellationTokenSource _tokenSource = new();

    public ProxyConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
    {
        Routes = routes;
        Clusters = clusters;
        ChangeToken = new CancellationChangeToken(_tokenSource.Token);
    }

    public IReadOnlyList<RouteConfig> Routes { get; }
    public IReadOnlyList<ClusterConfig> Clusters { get; }
    public IChangeToken ChangeToken { get; }

    public void OnChangedSignal()
    => _tokenSource.Cancel();
}