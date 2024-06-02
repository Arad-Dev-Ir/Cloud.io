namespace APIGateway.Endpoint.APIs;

using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

public class ProxyConfig : IProxyConfig
{
    public IReadOnlyList<RouteConfig> Routes { get; }
    public IReadOnlyList<ClusterConfig> Clusters { get; }
    public IChangeToken ChangeToken { get; }

    private readonly CancellationTokenSource _tokenSource = new();

    public ProxyConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
    {
        Routes = routes;
        Clusters = clusters;
        ChangeToken = new CancellationChangeToken(_tokenSource.Token);
    }

    public void OnChangeSignal()
    => _tokenSource.Cancel();
}