namespace Cloudio.Web.Services.Registry;

public static partial class RegistryExtensions
{
    public static IServiceCollection ConfigureServiceRegistry(this IServiceCollection services, Action<AppRegistryConfigs> act)
    => services.AddSingleton<IServiceRegistry, ServiceRegistry>().Configure(act);
}