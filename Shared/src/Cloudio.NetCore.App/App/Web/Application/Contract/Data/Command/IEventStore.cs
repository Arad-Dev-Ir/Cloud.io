namespace Cloudio.Web.Core.Contract;

using Cloudio.Core.Models;

public interface IEventStore
{
    void Save<TEvent>(string entity, long id, IEnumerable<TEvent> events) where TEvent : Event;
    Task SaveAsync<E>(string entity, long id, IEnumerable<E> events) where E : Event;

    IEnumerable<TEvent> Get<TEvent>(long id, string entity) where TEvent : Event;
}