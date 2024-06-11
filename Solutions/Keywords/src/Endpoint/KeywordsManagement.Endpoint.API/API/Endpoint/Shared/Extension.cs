namespace KeywordsManagement.Endpoint.APIs;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using Steeltoe.Discovery.Client;
using Cloud.Core.Extensions.Caching;
using Cloud.Core.Extensions.Serialization;
using Cloud.Core.Extensions.Identity;
using Cloud.Web.Data.Sql.Command;
using Cloud.Web.Endpoint.API;
using System.IO.Compression;
using Data.Sql.Commands;
using Data.Sql.Queries;
using Keyword.APIs;

public static class Extension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder source)
    {
        var result = default(WebApplication);
        var configuration = source.Configuration;

        source.Services.AddApiConfiguration(["Cloud", "KeywordsManagement"])
        .AddEndpointsApiExplorer()
        .AddHttpContextAccessor()
        .AddUserIdentity(e => configuration.GetSection("WebUserInfo").Bind(e))
        .AddMicrosoftSerializer()
        .AddInMemoryCache()
        .AddDbContext(configuration)
        .AddDiscoveryClient(configuration)
        //.AddHostedService<KeywordCreationEventPublisher>()
        .AddResponseCompression()
        .AddSwaggerGen();

        result = source.Build();
        return result;
    }

    public static WebApplication ConfigurePipelines(this WebApplication source)
    {
        var result = default(WebApplication);
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
        result = source;
        return result;
    }

    #region Private

    private static IServiceCollection AddDbContext(this IServiceCollection source, IConfiguration configuration)
    {
        source.AddDbContext<KeywordsManagementCommandContext>(e =>
        e.UseSqlServer(configuration.GetConnectionString("KeywordsManagementCommandDb_ConnectionString"))
        .AddInterceptors(new OutboxEventInterceptor())
        .SetDatabaseLogOptions()
        );

        source.AddDbContext<KeywordsManagementQueryContext>(e =>
        e.UseSqlServer(configuration.GetConnectionString("KeywordsManagementQueryDb_ConnectionString"))
        .SetDatabaseLogOptions()
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

    private static WebApplication UseCorsPolicy(this WebApplication source)
    {
        source.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        var result = source;
        return result;
    }

    private static async void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await scope.ServiceProvider
        .GetRequiredService<KeywordsManagementCommandContext>()
        .Database
        .MigrateAsync();
    }

    #endregion
}