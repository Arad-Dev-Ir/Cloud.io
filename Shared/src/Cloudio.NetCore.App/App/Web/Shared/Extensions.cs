namespace Cloudio.Web;

using System.Reflection;

public static partial class Extensions
{
    public static IServiceCollection Scan(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime, params Type[] assignableTo)
    {
        var result = services
        .Scan
        (
        e => e.FromAssemblies(assemblies)
        .AddClasses(e => e.AssignableToAny(assignableTo))
        .AsImplementedInterfaces()
        .WithLifetime(lifetime)
        );

        return result;
    }
}