namespace Cloudio.Web.Data.Sql.Command;

using Cloudio.Core.Models;

public class OutboxEvent : Model
{
    public long Id { get; set; }

    public string Name { get; set; } = Empty;

    public string Type { get; set; } = Empty;

    public string Payload { get; set; } = Empty;

    public string UserId { get; set; } = Empty;

    public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;

    public EventProcessingState State { get; set; } = EventProcessingState.Raised;


    public string ModuleCode { get; set; } = Empty;

    public string ModuleName { get; set; } = Empty;

    public string ModuleType { get; set; } = Empty;
}