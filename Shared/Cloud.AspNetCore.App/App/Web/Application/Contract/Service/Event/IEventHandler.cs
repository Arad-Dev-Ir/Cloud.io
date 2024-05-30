namespace Cloud.Web.Core.Contract;

using Cloud.Core.Models;

public interface IEventHandler<E> where E : Event
{
    Task ExecuteAsync(E @event);
}
