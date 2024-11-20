namespace Cloudio.Core.Services.Identity;

public interface IUserIdentity
{
    string GetUserAgent();
    string GetUserIp();
    string GetUserId();
    string GetUsername();
    string GetName();
    string GetFamily();
    string? GetClaim(string claimType);
    bool IsCurrentUser(string userId);
    string UserIdOrDefault();
    string UserIdOrDefault(string defaultValue);
}