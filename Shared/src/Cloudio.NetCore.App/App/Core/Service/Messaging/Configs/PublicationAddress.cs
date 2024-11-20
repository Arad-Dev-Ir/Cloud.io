namespace Cloudio.Core.Services.Messaging;

public class PublicationAddress
{
    public string ExchangeName { get; set; } = "";

    public string ExchangeType { get; set; } = "";

    public string RoutingKey { get; set; } = "";
}
