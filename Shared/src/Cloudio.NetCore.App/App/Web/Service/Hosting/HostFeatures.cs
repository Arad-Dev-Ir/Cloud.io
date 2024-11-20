namespace Cloudio.Web.Services.Hosting;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

public class HostFeatures(IServer server)
{
    private readonly IServer _server = server;

    public string GetUri()
    {
        var uri = _server
        .Features
        .Get<IServerAddressesFeature>()?
        .Addresses.FirstOrDefault();

        var result = uri ?? string.Empty;
        return result;
    }
}
