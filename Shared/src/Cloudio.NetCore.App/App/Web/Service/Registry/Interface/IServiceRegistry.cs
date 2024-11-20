namespace Cloudio.Web.Services.Registry;

public interface IServiceRegistry
{
    Task Register(Uri uri);

    Task Deregister(Uri uri);
}