namespace Cloudio.Web.Core.AppService;

using Cloudio.Core.Models;

public interface IEventController
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken token) where TEvent : IEvent;
}