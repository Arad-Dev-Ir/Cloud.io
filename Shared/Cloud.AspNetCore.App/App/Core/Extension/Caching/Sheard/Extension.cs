namespace Cloud.Core.Extensions.Caching;

using Microsoft.Extensions.DependencyInjection;

public static partial class Extension
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection source)
    => source.AddMemoryCache().AddTransient<ICache, MemoryCache>();

    public static IServiceCollection AddFakeMemoryCache(this IServiceCollection source)
    => source.AddTransient<ICache, FakeCache>();
}