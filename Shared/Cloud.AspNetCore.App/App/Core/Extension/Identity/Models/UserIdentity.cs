namespace Cloud.Core.Extensions.Identity;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Cloud.Core.Models;

public class UserIdentity : Model, IUserIdentity
{
    public UserIdentity(IHttpContextAccessor accessor, IOptions<UserConfig> options)
    {
        _accessor = accessor;
        _userOptions = options.Value;
    }

    private readonly IHttpContextAccessor _accessor;
    private readonly UserConfig _userOptions;
    private ClaimsPrincipal? User => GetHttpContext()?.User;

    public string GetUserId()
    => GetClaim(ClaimTypes.NameIdentifier) ?? Empty;

    public string UserIdOrDefault()
   => UserIdOrDefault(_userOptions.DefaultId);

    public string UserIdOrDefault(string defaultValue)
    => GetUserId().IsEmpty() ? defaultValue : GetUserId();

    public string GetUserIp()
    => GetHttpContext()?.Connection?.RemoteIpAddress?.ToString() ?? "0.0.0.0";

    public string GetUsername()
    => GetClaim(ClaimTypes.Name) ?? Empty;

    public string GetUserAgent()
    => GetHttpContext()?.Request.Headers.UserAgent.ToString() ?? Empty;

    public string GetName()
    => GetClaim(ClaimTypes.GivenName) ?? Empty;

    public string GetFamily()
    => GetClaim(ClaimTypes.Surname) ?? Empty;

    public string? GetClaim(string claimType)
    => User?.GetClaim(claimType);

    public bool IsCurrentUser(string userId)
    => string.Equals(GetUserId(), userId, StringComparison.OrdinalIgnoreCase);

    private HttpContext GetHttpContext()
    => _accessor.HttpContext;
}
