namespace Cloudio.Gateway.Endpoint;

using Yarp.ReverseProxy.Configuration;
using Cloudio.Core.Services.Identity;
using Cloudio.Core.Services.Serialization;
using Cloudio.Core.Services.Caching;
using Cloudio.Web.Endpoint.API;

internal static partial class Extensions
{
    internal static WebApplication ConfigureServices(this WebApplicationBuilder webApp)
    {
        WebApplication result;

        var configuration = webApp.Configuration;
        var services = webApp.Services;
        services.ConfigureGlobalExceptionHandling()
        .ConfigureRedisCache(e => configuration.GetSection(RedisServerConfigs.Section).Bind(e))
        .ConfigureBasicApiDependencies(["Cloudio", "APIGateway"])
        .AddEndpointsApiExplorer()
        .AddHttpContextAccessor()
        .ConfigureUserIdentity(e => configuration.GetSection("WebUserInfo").Bind(e))
        .ConfigureSerializer(SerializationMethod.Newtonsoft)
        .ConfigureInMemoryCache()
        .ConfigureCors()
        .ConfigureServiceDiscoveryProxy()
        .AddSwaggerGen();

        result = webApp.Build();
        return result;
    }

    internal static WebApplication ConfigurePipelines(this WebApplication app)
    {
        app.UseExceptionHandler();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("AllowOrigin");
        app.UseHttpsRedirection();
        app.MapReverseProxy();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}

internal static partial class Extensions
{
    internal static IServiceCollection ConfigureGlobalExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>()
        .AddProblemDetails();

        return services;
    }

    internal static IServiceCollection ConfigureServiceDiscoveryProxy(this IServiceCollection services)
    {
        services.AddReverseProxy();
        services.AddSingleton<IServiceDiscovery, ServiceDiscovery>()
        .AddSingleton<IProxyConfigProvider>(e => e.GetRequiredService<IServiceDiscovery>())
        .AddHostedService<DiscoveryHostedService>();

        return services;
    }

    internal static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        return services;
    }
}