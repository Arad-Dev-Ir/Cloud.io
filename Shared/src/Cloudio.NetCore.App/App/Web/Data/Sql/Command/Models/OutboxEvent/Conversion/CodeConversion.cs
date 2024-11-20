namespace Cloudio.Web.Data.Sql.Command;

internal class ProcessModeConversion : Conversion<EventProcessingState, string>
{
    public ProcessModeConversion() : base(e => e.Value, e => EventProcessingState.Instance(e))
    { }
}