namespace Cloudio.Core.Services.Messaging;

public interface IMessagePublisher : IMessageQueue
{
    void Publish(byte[] messageBytes);
}