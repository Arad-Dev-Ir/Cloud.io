namespace KeywordsManagement.Data.External.Messaging;

using Cloudio.Core;

public class EventPublisherHostedService(KeywordsEventPublisher publisher) : HostedBackgroundService
{
    private const int MillisecondsDelay = 30000;

    private readonly KeywordsEventPublisher _publisher = publisher;

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await _publisher.Publish(token);
            await Task.Delay(MillisecondsDelay, token);
        }

        _publisher.Close();
    }
}