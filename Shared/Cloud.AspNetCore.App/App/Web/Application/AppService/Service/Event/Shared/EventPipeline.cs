namespace Cloud.Web.Core.AppService;

using Cloud.Core.Models;

public abstract class EventPipeline(IServiceProvider serviceProvider) : Procedure<EventPipeline>(serviceProvider)
{

    public abstract Task ExecuteAsync<E>(E @event) where E : Event;
}