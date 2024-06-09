namespace NewsManagement.Endpoint.APIs;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using Steeltoe.Discovery.Client;
using Cloud.Core.Extensions.Serialization;
using Cloud.Core.Extensions.Caching;
using Cloud.Core.Extensions.Identity;
using Cloud.Web.Data.Sql.Command;
using Cloud.Web.Endpoint.API;
using System.IO.Compression;
using Data.Sql.Commands;
using Data.Sql.Queries;
using News.APIs;

public static class Extension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder source)
    {
        var result = default(WebApplication);

        var configuration = source.Configuration;
        source.Services.AddApiConfiguration(["Cloud", "NewsManagement"])
        .AddEndpointsApiExplorer()
        .AddHttpContextAccessor()
        .AddUserIdentity(e => configuration.GetSection("WebUserInfo").Bind(e))
        .AddMicrosoftSerializer()
        .AddInMemoryCache()
        .AddDbContext(configuration)
        //.AddDiscoveryClient(configuration)
        //.AddHostedService<KeywordCreationEventReceiver>()
        .AddResponseCompression()
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
        source.UseAuthorization();
        source.MapControllers();
        source.UseResponseCompression();
        source.MigrateDatabase();
        return source;
    }

    #region Private

    private static IServiceCollection AddDbContext(this IServiceCollection source, IConfiguration configuration)
    {
        source.AddDbContext<NewsManagementCommandContext>(e =>
        e.UseSqlServer(configuration.GetConnectionString("NewsManagementCommandDb_ConnectionString"))
        .AddInterceptors(new OutboxEventInterceptor())
        .SetDatabaseOptions()
        );

        source.AddDbContext<NewsManagementQueryContext>(e =>
        e.UseSqlServer(configuration.GetConnectionString("NewsManagementQueryDb_ConnectionString"))
        .SetDatabaseOptions()
        );
        return source;
    }

    private static IServiceCollection AddResponseCompression(this IServiceCollection source)
    {
        source.AddResponseCompression(e =>
        {
            e.EnableForHttps = true;
            e.Providers.Add<GzipCompressionProvider>();
            e.Providers.Add<BrotliCompressionProvider>();
        });

        source.Configure<GzipCompressionProviderOptions>(e => e.Level = CompressionLevel.Optimal);
        source.Configure<BrotliCompressionProviderOptions>(e => e.Level = CompressionLevel.Optimal);
        return source;
    }

    private static IApplicationBuilder UseCorsPolicy(this WebApplication source)
    {
        source.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        return source;
    }

    private static async void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await scope.ServiceProvider
        .GetRequiredService<NewsManagementCommandContext>()
        .Database
        .MigrateAsync();
    }

    #endregion
}