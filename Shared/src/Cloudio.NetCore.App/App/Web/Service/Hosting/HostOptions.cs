namespace Cloudio.Web.Services.Hosting;

using Cloudio.Core.Models;

public record HostOptions : DataTransferObject
{
    public string ProjectPath { get; set; } = null!;
    public int Port { get; set; }
}