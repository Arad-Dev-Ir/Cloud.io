namespace Cloudio.Web.Core;

using Cloudio.Core.Models;

public abstract class Module : Entity, IModule
{
    private readonly List<IEvent> events;

    public IReadOnlyCollection<IEvent> Events
    => events;

    protected Module()
    => events = [];

    protected void AddEvent<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IEvent
    => events.Add(@event);

    public void ClearEvents()
    => events.Clear();
}