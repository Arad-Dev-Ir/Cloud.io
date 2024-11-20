namespace Cloudio.Web.Services.Registry;

using Cloudio.Core;
using Cloudio.Web.Services.Hosting;

public class RegistryHostedService(IServiceRegistry serviceRegstry, HostFeatures hostFeatures) : IHostedLifespanService
{
    private readonly IServiceRegistry _serviceRegistry = serviceRegstry;
    private readonly HostFeatures _hostFeatures = hostFeatures;

    public async Task StartedAsync(CancellationToken token)
    {
        var uri = GetUri();
        if (uri.IsEmpty()) return;

        await _serviceRegistry.Register(new(uri));
    }

    public async Task StoppedAsync(CancellationToken token)
    {
        var uri = GetUri();
        if (uri.IsEmpty()) return;

        await _serviceRegistry.Deregister(new(uri));
    }

    #region Uri

    private string GetUri()
    => _hostFeatures.GetUri();

    #endregion
}