namespace Cloudio.Web.Services.Registry;

using Cloudio.Core.Models;

public class AppRegistryConfigs : Model
{
    public const string Section = "AppRegistryConfigs";

    public string Prefix { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string UpstreamRoute { get; set; } = null!;

    public string Key => $"{Prefix}.{Name}";
}