namespace NewsManagement.Endpoint;

using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using Cloudio.Core.Services.Messaging;
using Cloudio.Core.Services.Caching;
using Cloudio.Core.Services.Serialization;
using Cloudio.Core.Services.Identity;
using Cloudio.Web.Services.Registry;
using Cloudio.Web.Endpoint.API;
using NewsManagement.Data.Sql.Commands;
using NewsManagement.Data.Sql.Queries;
using NewsManagement.Data.External.Messaging;

internal static partial class Extensions
{
    internal static WebApplication ConfigureServices(this WebApplicationBuilder webApp)
    {
        WebApplication result;

        var configuration = webApp.Configuration;
        var services = webApp.Services;

        services.ConfigureBasicApiDependencies(["Cloud", "NewsManagement"])
       .ConfigureGlobalExceptionHandling()
       .ConfigureServiceRegistry(configuration)
       .AddEndpointsApiExplorer()
       .AddHttpContextAccessor()
       .ConfigureUserIdentity(e => configuration.GetSection("WebUserInfo").Bind(e))
       .ConfigureSerializer(SerializationMethod.Newtonsoft)
       .ConfigureInMemoryCache()
       .ConfigureDbContext(configuration)
       .ConfigureCors()
       .ConfigureMessageConsumer(configuration)
       .ConfigureHostedService()
       .ConfigureResponseCompression()
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
        app.UseAuthorization();
        app.MapControllers();
        app.UseResponseCompression();
        app.MigrateDatabase();

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

    internal static IServiceCollection ConfigureServiceRegistry(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.ConfigureRedisCache(e => configuration.GetSection(RedisServerConfigs.Section).Bind(e))
        .ConfigureServiceRegistry(e => configuration.GetSection(AppRegistryConfigs.Section).Bind(e))
        .AddHostedService<RegistryHostedService>();

        return services;
    }

    internal static IServiceCollection ConfigureDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.ConfigureCommandDbContext(configuration);
        services.ConfigureQueryDbContext(configuration);

        return services;
    }

    internal static IServiceCollection ConfigureMessageConsumer(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
        .ConfigureRabbitMessageConsumer(e =>
        {
            configuration.GetSection(MessageQueueConfigs.Section).Bind(e);
        })
        .ConfigureKeywordsEventConsumer(e =>
        {
            e.ConnectionString = configuration.GetConnectionString("NewsManagementCommandDb_ConnectionString")!;
        });

        return services;
    }

    internal static IServiceCollection ConfigureHostedService(this IServiceCollection services)
    => services.ConfigureEventConsumerHostedService();

    internal static IServiceCollection ConfigureResponseCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(e =>
        {
            e.EnableForHttps = true;
            e.Providers.Add<GzipCompressionProvider>();
            e.Providers.Add<BrotliCompressionProvider>();
        });

        services.Configure<GzipCompressionProviderOptions>(e => e.Level = CompressionLevel.Optimal);
        services.Configure<BrotliCompressionProviderOptions>(e => e.Level = CompressionLevel.Optimal);

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

    internal static async void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await scope.ServiceProvider
        .GetRequiredService<NewsManagementCommandContext>()
        .Database
        .MigrateAsync();
    }
}