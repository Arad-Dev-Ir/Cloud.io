namespace Cloud.Web.Core.Contract;

using Cloud.Core.Models;

public interface IEventStore
{
    void Save<E>(string entity, long id, IEnumerable<E> events) where E : Event;
    Task SaveAsync<E>(string entity, long id, IEnumerable<E> events) where E : Event;

    IEnumerable<E> Get<E>(long id, string entity) where E : Event;
}