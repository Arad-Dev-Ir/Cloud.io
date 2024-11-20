namespace Cloudio.Web.Data.Sql;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Cloudio.Web.Core.Contract;

public static partial class Extensions
{
    public static DbContextOptionsBuilder ConfigureDatabaseOptions(this DbContextOptionsBuilder options)
    {
        var result = options
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();

        return result;
    }
}

public static partial class Extensions
{
    public static IServiceCollection SetDataLayerDependencies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var result = services.SetRepositoryDependencies(assemblies)
        .SetUnitOfWorkDependencies(assemblies);

        return result;
    }

    #region Private Methods

    private static IServiceCollection SetRepositoryDependencies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    => services.Scan(assemblies, ServiceLifetime.Transient, typeof(ICommandRepository<>), typeof(IQueryRepository));

    private static IServiceCollection SetUnitOfWorkDependencies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    => services.Scan(assemblies, ServiceLifetime.Scoped, typeof(IUnitOfWork));

    #endregion
}
