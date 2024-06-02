namespace APIGateway.Endpoint.APIs;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Steeltoe.Discovery.Client;
using Yarp.ReverseProxy.Configuration;
using Cloud.Web.Endpoint.API;
using Cloud.Core.Extensions.Identity;
using Cloud.Core.Extensions.Serialization;
using Cloud.Core.Extensions.Caching;

public static partial class Extension
{
    // hosting
    public static WebApplication ConfigureServices(this WebApplicationBuilder source)
    {
        var result = default(WebApplication);

        var configuration = source.Configuration;
        source.Services.AddApiConfiguration(["Cloud", "APIGateway"])
        .AddEndpointsApiExplorer()
        .AddHttpContextAccessor()
        .AddUserIdentity(e => configuration.GetSection("WebUserInfo").Bind(e))
        .AddMicrosoftSerializer()
        .AddInMemoryCache()
        .AddDbContext(configuration)
        .AddDiscoveryClient()
        .AddProxy()
        .AddSwaggerGen();

        result = source.Build();
        return result;
    }

    public static WebApplication ConfigurePipelines(this WebApplication source)
    {
        source.UseApiException();
        if (source.Environment.IsDevelopment())
        {
            source.UseSwagger();
            source.UseSwaggerUI();
        }
        source.UseCorsPolicy();
        source.UseHttpsRedirection();
        source.MapReverseProxy();
        source.UseAuthorization();
        source.MapControllers();
        return source;
    }

    #region Private

    private static IServiceCollection AddDbContext(this IServiceCollection source, IConfiguration configuration)
    {
        //source.AddDbContext<ApiGatewayCommandContext>(e =>
        //e.UseSqlServer(configuration.GetConnectionString("ApiGatewayCommandDb_ConnectionString"))
        //.AddInterceptors(new OutboxEventInterceptor()));

        //source.AddDbContext<ApiGatewayQueryContext>(e =>
        //e.UseSqlServer(configuration.GetConnectionString("ApiGatewayQueryDb_ConnectionString")));

        return source;
    }

    private static WebApplication UseCorsPolicy(this WebApplication source)
    {
        source.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        return source;
    }

    private static IServiceCollection AddProxy(this IServiceCollection source)
    {
        source.AddReverseProxy();
        source.AddSingleton<ServiceDiscoveryProxy>()
        .AddSingleton<IProxyConfigProvider>(e => e.GetRequiredService<ServiceDiscoveryProxy>())
        .AddSingleton<IHostedService>(e => e.GetRequiredService<ServiceDiscoveryProxy>());
        var result = source;
        return result;
    }

    #endregion
}