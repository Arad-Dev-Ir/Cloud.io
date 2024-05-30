namespace Cloud.Web.Endpoint.API;

using System.Security.Claims;

public static partial class ClaimsExtension
{
    public static string? GetClaim(this ClaimsPrincipal source, string type)
    => source.Claims?.FirstOrDefault((Claim x) => x.Type == type)?.Value;
}