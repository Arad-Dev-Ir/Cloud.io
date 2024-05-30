namespace Cloud.Web.Endpoint.API;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Web.Core.Contract;

public static partial class DataAccessExtension
{
    public static IServiceCollection SetDataAccessDependencies(this IServiceCollection source, IEnumerable<Assembly> assemblies)
    => source.SetRepositories(assemblies).SetUnitOfWork(assemblies);

    #region private

    private static IServiceCollection SetRepositories(this IServiceCollection source, IEnumerable<Assembly> assemblies)
    => source.SetWithLifetime(assemblies, Lifetime.Transient, typeof(ICommandRepository<>), typeof(IQueryRepository));

    private static IServiceCollection SetUnitOfWork(this IServiceCollection source, IEnumerable<Assembly> assemblies)
    => source.SetWithLifetime(assemblies, Lifetime.Scoped, typeof(IUnitOfWork));

    #endregion
}