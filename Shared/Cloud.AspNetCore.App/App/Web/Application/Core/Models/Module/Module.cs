namespace Cloud.Web.Core;

using Cloud.Core.Models;

public class Module : Entity
{
    //public int Version { get; private set; }

    protected Module()
    => events = [];

    public Module(IEnumerable<IEvent> events)
    {
        if (events is null || !events.Any()) return;
        foreach (var item in events)
        {
            Assign(item);
            //Versioner();
        }
    }

    protected void Perform(IEvent @event)
    {
        AddEvent(@event);
        Assign(@event);
    }

    private readonly List<IEvent> events;
    public IEnumerable<IEvent> GetEvents()
    => events.AsEnumerable();

    protected void AddEvent(IEvent @event)
    => events.Add(@event);

    public void ClearEvents()
    => events.Clear();

    private void Assign(IEvent @event)
    => CallMethod("OnProcessEvent", @event.GetType(), new[] { @event });

    //private void Versioner()
    //=> Version++;
}
