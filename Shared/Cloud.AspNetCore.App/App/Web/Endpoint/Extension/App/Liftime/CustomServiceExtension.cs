namespace Cloud.Web.Endpoint.API;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Cloud.Core.Extensions.Lifetime;

public static partial class CustomServiceExtension
{
    public static IServiceCollection SetCustomDependencies(this IServiceCollection source, List<Assembly> assemblies)
    => source
       .SetWithLifetime(assemblies, Lifetime.Scoped, typeof(IScoped))
       .SetWithLifetime(assemblies, Lifetime.Singleton, typeof(ISingleton))
       .SetWithLifetime(assemblies, Lifetime.Transient, typeof(ITransient));
}