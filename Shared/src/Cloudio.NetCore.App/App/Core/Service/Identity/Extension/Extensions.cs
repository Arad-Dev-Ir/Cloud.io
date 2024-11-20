namespace Cloudio.Core.Services.Identity;

using System.Security.Claims;

public static partial class Extensions
{
    public static IServiceCollection ConfigureUserIdentity(this IServiceCollection services, Action<UserConfig> act)
    => services.AddTransient<IUserIdentity, UserIdentity>().Configure(act);
}

public static partial class Extensions
{
    public static string? GetClaim(this ClaimsPrincipal source, string type)
    {
        var result = source.Claims.FirstOrDefault(e => e.Type == type)?.Value;
        return result;
    }
}