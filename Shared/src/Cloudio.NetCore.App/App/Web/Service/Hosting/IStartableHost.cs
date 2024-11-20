namespace Cloudio.Web.Services.Hosting;

public interface IHost
{
    string BaseUrl { get; }
}

public interface IStartableHost : IHost
{
    void Up();
    void Down();
}