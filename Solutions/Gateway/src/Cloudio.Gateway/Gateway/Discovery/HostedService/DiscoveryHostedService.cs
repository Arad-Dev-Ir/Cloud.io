namespace Cloudio.Gateway.Endpoint;

using Cloudio.Core;

public class DiscoveryHostedService(IServiceDiscovery serviceDiscovery) : HostedBackgroundService
{
    private const int MillisecondsDelay = 30000;

    private readonly IServiceDiscovery _serviceDiscovery = serviceDiscovery;

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await _serviceDiscovery.DiscoverAsync();
            await Task.Delay(MillisecondsDelay, token);
        }
    }
}