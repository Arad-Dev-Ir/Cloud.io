namespace KeywordsManagement.Data.External.Messaging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Cloudio.Core;
using Cloudio.Core.Services.Messaging;
using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Data.Sql.Commands;
using Cloudio.Core.Services.Serialization;
using KeywordsManagement.Core.Keyword.Models;

public class KeywordsEventPublisher
{
    private const string Keyword = nameof(Keyword);

    private readonly IMessagePublisher _messagePublisher;
    private readonly IServiceProvider _serviceProvider;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly KeywordsManagementCommandDbContext _context;

    public KeywordsEventPublisher(IMessagePublisher messagePublisher, IServiceProvider serviceProvider, IJsonSerializer jsonSerializer)
    {
        _messagePublisher = messagePublisher;
        _serviceProvider = serviceProvider;
        _jsonSerializer = jsonSerializer;
        var scope = _serviceProvider.CreateAsyncScope();
        _context = scope.ServiceProvider.GetRequiredService<KeywordsManagementCommandDbContext>();
    }

    public async Task PublishOldCorrect(CancellationToken token)
    {
        var events = _context.Events
        .Where(e => e.State == EventProcessingState.Raised && e.ModuleName == Keyword)
        .Take(100);

        foreach (var item in events)
        {
            _messagePublisher.Publish(item.Payload.Bytes());
        }

        await events.ExecuteUpdateAsync(e => e.SetProperty(o => o.State, EventProcessingState.Registered), token);
    }

    public async Task Publish(CancellationToken token)
    {
        var outboxEvents = _context.Events
        .Where(e => e.State == EventProcessingState.Raised && e.ModuleName == Keyword)
        .Take(100);

        foreach (var item in outboxEvents)
        {
            var payload = item.Payload;
            var @event = _jsonSerializer.Deserialize<Event>(payload);

            var isActive = @event!.State.Is(KeywordState.Active.Value);
            if (isActive)
            {
                _messagePublisher.Publish(payload.Bytes());
            }
        }

        await outboxEvents.ExecuteUpdateAsync(e => e.SetProperty(o => o.State, EventProcessingState.Registered), token);
    }

    public void Close()
    => _messagePublisher.Close();

    #region Event
    private record Event(string State);
    #endregion
}