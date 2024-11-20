namespace NewsManagement.Data.External.Messaging;

using System;
using Microsoft.Extensions.DependencyInjection;
using Cloudio.Core;
using Cloudio.Core.Services.Messaging;
using Cloudio.Core.Services.Serialization;
using Cloudio.Web.Core.AppService;

public class KeywordsEventConsumer
{
    private readonly IServiceProvider _ServoceProvider;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IMessageConsumer _messageConsumer;
    private readonly IEventController _eventController;

    public KeywordsEventConsumer(IMessageConsumer messageConsumer, IServiceProvider provider, IJsonSerializer serializer)
    {
        _messageConsumer = messageConsumer;
        _ServoceProvider = provider;
        _jsonSerializer = serializer;

        var scope = _ServoceProvider.CreateAsyncScope();
        _eventController = scope.ServiceProvider.GetRequiredService<IEventController>();
    }

    public async Task Consume(CancellationToken token)
    {
        _messageConsumer.Consume(async e =>
        {
            var entity = _jsonSerializer.Deserialize<Keyword>(e.Body.ToArray().String());
            if (entity is null) return;

            await _eventController.PublishAsync(entity, token);
        });

        await Task.CompletedTask;
    }

    public void Close()
     => _messageConsumer.Close();
}