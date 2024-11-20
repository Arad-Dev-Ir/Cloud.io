namespace Cloudio.Gateway.Endpoint;

using Yarp.ReverseProxy.Configuration;

public interface IServiceDiscovery : IProxyConfigProvider
{
    Task DiscoverAsync();
}