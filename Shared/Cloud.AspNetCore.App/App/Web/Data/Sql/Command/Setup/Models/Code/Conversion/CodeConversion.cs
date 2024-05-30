namespace Cloud.Web.Data.Sql.Command;

using Cloud.Core.Models;

internal class CodeConversion : Conversion<Code, Guid>
{
    public CodeConversion() : base(e => e.Value, e => Code.Instance(e))
    { }
}
