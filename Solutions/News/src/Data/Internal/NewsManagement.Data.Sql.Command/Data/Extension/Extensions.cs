namespace NewsManagement.Data.Sql.Commands;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cloudio.Web.Data.Sql;
using Cloudio.Web.Data.Sql.Command;

public static partial class Extensions
{
    public static IServiceCollection ConfigureCommandDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<NewsManagementCommandContext>(e =>
        e.UseSqlServer(configuration.GetConnectionString("NewsManagementCommandDb_ConnectionString"))
        .AddInterceptors(new OutboxEventInterceptor())
        .ConfigureDatabaseOptions()
        );

        return services;
    }
}