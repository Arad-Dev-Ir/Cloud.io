namespace Cloudio.Core.Services.Identity;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Cloudio.Core.Models;

public class UserIdentity(IHttpContextAccessor accessor, IOptionsMonitor<UserConfig> options) : Model, IUserIdentity
{
    private readonly IHttpContextAccessor _accessor = accessor;
    private readonly UserConfig _userOptions = options.CurrentValue;
    private ClaimsPrincipal? User => GetHttpContext()?.User;

    public string GetUserId()
    {
        var result = GetClaim(ClaimTypes.NameIdentifier) ?? Empty;
        return result;
    }

    public string UserIdOrDefault()
    {
        var result = UserIdOrDefault(_userOptions.DefaultId);
        return result;
    }

    public string UserIdOrDefault(string defaultValue)
    {
        var result = GetUserId().IsEmpty() ? defaultValue : GetUserId();
        return result;
    }

    public string GetUserIp()
    {
        var result = GetHttpContext()?.Connection?.RemoteIpAddress?.ToString() ?? "0.0.0.0";
        return result;
    }

    public string GetUsername()
    {
        var result = GetClaim(ClaimTypes.Name) ?? Empty;
        return result;
    }

    public string GetUserAgent()
    {
        var result = GetHttpContext()?.Request.Headers.UserAgent.ToString() ?? Empty;
        return result;
    }

    public string GetName()
    {
        var result = GetClaim(ClaimTypes.GivenName) ?? Empty;
        return result;
    }

    public string GetFamily()
    {
        var result = GetClaim(ClaimTypes.Surname) ?? Empty;
        return result;
    }

    public string? GetClaim(string claimType)
    {
        var result = User?.GetClaim(claimType);
        return result;
    }

    public bool IsCurrentUser(string userId)
    {
        var result = string.Equals(GetUserId(), userId, StringComparison.OrdinalIgnoreCase);
        return result;
    }

    private HttpContext GetHttpContext()
    => _accessor.HttpContext!;
}
