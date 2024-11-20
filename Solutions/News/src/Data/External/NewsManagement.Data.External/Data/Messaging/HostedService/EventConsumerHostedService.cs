namespace NewsManagement.Data.External.Messaging;

using Cloudio.Core;

public class EventConsumerHostedService(KeywordsEventConsumer eventConsumer) : HostedBackgroundService
{
    private const int MillisecondsDelay = 3000;

    private readonly KeywordsEventConsumer _eventConsumer = eventConsumer;

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        await _eventConsumer.Consume(token);

        while (!token.IsCancellationRequested)
            await Task.Delay(MillisecondsDelay, token);

        _eventConsumer.Close();
    }
}