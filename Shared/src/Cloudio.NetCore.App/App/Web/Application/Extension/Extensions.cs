namespace Cloudio.Web.Core;

using System.Reflection;
using FluentValidation;
using Cloudio.Core.Services.Lifetime;
using Cloudio.Web.Core.AppService;
using Cloudio.Web.Core.Contract;

public static partial class Extensions
{
    internal static IServiceCollection SetAppServiceLayerDependencies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var result = services
        .SetRequestHandlers(assemblies)
        .SetRequestPipeline()
        .SetEventHandlers(assemblies)
        .SetEventController(assemblies)
        .SetFluentValidators(assemblies);

        return result;
    }

    #region Private Methods

    private static IServiceCollection SetRequestHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    => services.Scan(assemblies, ServiceLifetime.Transient, typeof(IRequestHandler<>), typeof(IRequestHandler<,>));

    private static IServiceCollection SetRequestPipeline(this IServiceCollection services)
    {
        services.AddTransient<RequestController>();
        services.AddTransient<RequestValidation>();

        services.AddTransient<IRequestController>(e =>
        {
            var requestController = e.GetRequiredService<RequestController>();
            var requestValidation = e.GetRequiredService<RequestValidation>();

            requestValidation
            .SetNext(requestController);

            var result = requestValidation;
            return result;
        });

        return services;
    }

    private static IServiceCollection SetEventHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    => services.Scan(assemblies, ServiceLifetime.Transient, typeof(IEventHandler<>));

    private static IServiceCollection SetEventController(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    => services.Scan(assemblies, ServiceLifetime.Singleton, typeof(IEventController));

    private static IServiceCollection SetFluentValidators(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    => services.AddValidatorsFromAssemblies(assemblies);

    #endregion
}

public static partial class Extensions
{
    public static IServiceCollection SetCustomDependencies(this IServiceCollection services, List<Assembly> assemblies)
    {
        var result = services
        .Scan(assemblies, ServiceLifetime.Scoped, typeof(IScoped))
        .Scan(assemblies, ServiceLifetime.Singleton, typeof(ISingleton))
        .Scan(assemblies, ServiceLifetime.Transient, typeof(ITransient));

        return result;
    }
}