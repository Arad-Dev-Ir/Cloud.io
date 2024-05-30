namespace Cloud.Core.Extensions.Identity;

using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

public static partial class Extension
{
    public static IServiceCollection AddUserIdentity(this IServiceCollection source, Action<UserConfig> act)
    => source.AddTransient<IUserIdentity, UserIdentity>().Configure(act);
}

public static partial class Extension
{
    public static string? GetClaim(this ClaimsPrincipal source, string type)
    => source.Claims.FirstOrDefault(e => e.Type == type)?.Value;
}
