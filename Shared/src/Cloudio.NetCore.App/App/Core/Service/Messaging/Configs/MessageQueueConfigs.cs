namespace Cloudio.Core.Services.Messaging;

public class MessageQueueConfigs
{
    public const string Section = "MessageQueueConfigs";

    public string HostName { get; set; } = null!;

    public string ExchangeName { get; set; } = null!;

    public string ExchangeType { get; set; } = null!;

    public string RouteKey { get; set; } = null!;
}