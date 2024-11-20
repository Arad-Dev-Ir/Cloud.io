namespace Cloudio.Core.Services.Caching;

using Cloudio.Core.Models;

public class RedisServerConfigs : Model
{
    public const string Section = "RedisServerConfigs";

    public string Host { get; set; } = null!;

    public int Port { get; set; }

    public string Password { get; set; } = null!;
}