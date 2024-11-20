namespace Cloudio.Core.Services.Messaging;

public interface IMessageConsumer : IMessageQueue
{
    void Consume(Action<BasicDeliveryEventArgs> act);

    void Acknowledge(ulong deliveryTag, bool multiple);
}