namespace NewsManagement.Data.Sql.Queries;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cloudio.Web.Data.Sql;

public static class Extensions
{
    public static IServiceCollection ConfigureQueryDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<NewsManagementQueryContext>(e =>
        e.UseSqlServer(configuration.GetConnectionString("NewsManagementQueryDb_ConnectionString"))
        .ConfigureDatabaseOptions()
        );

        return services;
    }
}
