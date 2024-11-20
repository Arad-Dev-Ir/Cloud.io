namespace Cloudio.Web.Services.Registry;

using Cloudio.Core.Models;

public class AppHost : Model
{
    public string Name { get; set; } = null!;

    public string UpstreamRoute { get; set; } = null!;

    public List<Instance> Instances { get; set; } = [];
}

public class Instance : Model
{
    public string Scheme { get; set; } = null!;

    public string Host { get; set; } = null!;

    public int Port { get; set; }
}