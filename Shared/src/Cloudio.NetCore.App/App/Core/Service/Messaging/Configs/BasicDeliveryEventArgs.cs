namespace Cloudio.Core.Services.Messaging;

public class BasicDeliveryEventArgs : EventArgs
{
    public ReadOnlyMemory<byte> Body { get; set; } = default!;

    public BasicProperties BasicProperties { get; set; } = default!;

    public string ConsumerTag { get; set; } = "";

    public ulong DeliveryTag { get; set; }

    public string Exchange { get; set; } = "";

    public bool Redelivered { get; set; }

    public string RoutingKey { get; set; } = "";
}
