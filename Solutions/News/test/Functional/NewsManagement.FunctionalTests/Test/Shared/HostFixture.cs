namespace NewsManagement.Test.News.Functionals;

using Cloudio.Web.Services.Hosting;

public class HostFixture : IDisposable
{
    private readonly HostService _host;

    public HostFixture()
    {
        _host = new HostService(new()
        {
            ProjectPath = @"D:\Projects\Apps\Cloud.io\Solutions\News\src\Endpoint\NewsManagement.Endpoint.API\NewsManagement.Endpoint.API.csproj",
            Port = 7011
        });

        UpHost();
    }

    public void Dispose()
    {
        DownHost();
        GC.SuppressFinalize(this);
    }

    void UpHost()
    => _host.Up();

    void DownHost()
    => _host.Down();
}
