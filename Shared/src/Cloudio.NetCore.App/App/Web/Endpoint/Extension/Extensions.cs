namespace Cloudio.Web.Endpoint.API;

using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyModel;
using Cloudio.Web.Core;
using Cloudio.Web.Data.Sql;
using Cloudio.Web.Services.Hosting;
using Cloudio.Web.Core.AppService;

public static partial class Extensions
{
    public static IServiceCollection ConfigureBasicApiDependencies(this IServiceCollection services, IEnumerable<string> assemblyNames)
    {
        var result = services;

        result.AddControllers();
        result.SetApplicationDependencies(assemblyNames);
        result.SetHostFeatures();

        #region Client Side Validation
        //result.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters(); // That's for client side validation
        #endregion

        return result;
    }

    public static IServiceCollection SetHostFeatures(this IServiceCollection services)
    => services.AddSingleton<HostFeatures>();
}

public static partial class Extensions
{
    internal static IServiceCollection SetApplicationDependencies(this IServiceCollection services, IEnumerable<string> assemblyNames)
    {
        var assemblies = GetRequiredAssemblies(assemblyNames);

        services
        .SetAppServiceLayerDependencies(assemblies)
        .SetDataLayerDependencies(assemblies)
        .SetCustomDependencies(assemblies);

        return services;
    }

    #region Private Methods

    private static List<Assembly> GetRequiredAssemblies(IEnumerable<string> assemblyNames)
    {
        var result = new List<Assembly>();

        var libraries = DependencyContext.Default?.RuntimeLibraries;
        foreach (var item in libraries)
        {
            var isCandidate = IsCandidateAssemblyToLoad(item, assemblyNames);
            if (isCandidate)
                result.Add(Assembly.Load(new AssemblyName(item.Name)));
        }

        return result;
    }

    private static bool IsCandidateAssemblyToLoad(RuntimeLibrary library, IEnumerable<string> assemblyNames)
    => assemblyNames.Any(library.Name.Contains) || library.Dependencies.Any(e => assemblyNames.Any(a => e.Name.Contains(a)));

    #endregion
}

public static partial class Extensions
{
    public static IRequestController GetRequestController(this HttpContext httpContext)
    {
        var result = httpContext.RequestServices.GetRequiredService<IRequestController>();
        return result;
    }

    public static IEventController GetEventController(this HttpContext httpContext)
    {
        var result = httpContext.RequestServices.GetRequiredService<IEventController>();
        return result;
    }
}