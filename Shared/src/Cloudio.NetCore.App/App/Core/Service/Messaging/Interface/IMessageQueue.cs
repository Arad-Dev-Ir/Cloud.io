namespace Cloudio.Core.Services.Messaging;

public interface IMessageQueue
{
    public void Close();

    public void Dispose();
}