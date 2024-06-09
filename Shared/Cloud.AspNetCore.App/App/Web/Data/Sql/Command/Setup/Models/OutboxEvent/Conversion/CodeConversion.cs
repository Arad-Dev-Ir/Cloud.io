namespace Cloud.Web.Data.Sql.Command;

internal class ProcessModeConversion : Conversion<ProcessMode, string>
{
    public ProcessModeConversion() : base(e => e.Value, e => ProcessMode.Instance(e))
    { }
}