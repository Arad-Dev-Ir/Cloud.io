namespace Cloud.Web.Endpoint.API;

using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

public enum Lifetime : byte
{
    Singleton = 1,
    Scoped = 2,
    Transient = 3
}

public static partial class ServiceLifetimeConfigurationExtension
{
    public static IServiceCollection SetWithLifetime(this IServiceCollection source, IEnumerable<Assembly> assemblies, Lifetime lifetime, params Type[] assignableTo)
    {
        source
        .Scan
        (
        e => e.FromAssemblies(assemblies)
        .AddClasses(e => e.AssignableToAny(assignableTo))
        .AsImplementedInterfaces()
        .SetLifetime(lifetime)
        );
        return source;
    }

    private static IImplementationTypeSelector SetLifetime(this ILifetimeSelector source, Lifetime lifetime)
    {
        var result = lifetime switch
        {
            Lifetime.Singleton => source.WithSingletonLifetime(),
            Lifetime.Scoped => source.WithScopedLifetime(),
            Lifetime.Transient => source.WithTransientLifetime(),
            _ => source.WithTransientLifetime()
        };
        return result;
    }
}