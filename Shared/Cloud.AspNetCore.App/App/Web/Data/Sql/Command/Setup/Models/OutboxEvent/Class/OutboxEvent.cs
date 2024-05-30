namespace Cloud.Web.Data.Sql.Command;

using Cloud.Core.Models;

public class OutboxEvent : Model
{
    public long Id { get; set; }
    public string Name { get; set; } = Empty;
    public string Type { get; set; } = Empty;
    public string Data { get; set; } = Empty;
    public string UserId { get; set; } = Empty;
    public DateTime Date { get; set; }
    public ProcessMode Mode { get; set; } = ProcessMode.Raised;

    public string ModuleId { get; set; } = Empty;
    public string ModuleName { get; set; } = Empty;
    public string ModuleType { get; set; } = Empty;
}