namespace Cloudio.Client;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Cloudio.Core.Services.Serialization;
using Cloudio.Client.Endpoints;
using Cloudio.Client.Endpoints.Keywords;
using Cloudio.Client.Endpoints.News;
//
using Cloudio.Web.Endpoint.API;
//

internal static partial class EndpointExtensions
{
    internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
        .AddHttpClient()
        .AddEndpoints()
        .ConfigureSerializer(SerializationMethod.Newtonsoft)
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();

        var app = builder.Build();
        return app;
    }

    internal static WebApplication ConfigurePipelines(this WebApplication app)
    {
        app.UseExceptionHandler();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapEndpoints();
        return app;
    }
}

internal static partial class EndpointExtensions
{
    internal static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var endpoint = typeof(IEndpoint);
        var assembly = Assembly.GetExecutingAssembly(); ServiceDescriptor[] serviceDescriptors = assembly
        .DefinedTypes
        .Where(e => e is { IsAbstract: false, IsInterface: false } && e.IsAssignableTo(endpoint))
        .Select(e => ServiceDescriptor.Transient(endpoint, e))
        .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
        return services;
    }

    internal static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        IEnumerable<IEndpoint> endpoints = app
        .Services
        .GetRequiredService<IEnumerable<IEndpoint>>();
        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }

    internal static IServiceCollection AddHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<NewsClient>();
        services.AddHttpClient<KeywordsClient>();

        return services;
    }
}