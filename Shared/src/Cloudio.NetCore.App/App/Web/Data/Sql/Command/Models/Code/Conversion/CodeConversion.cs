namespace Cloudio.Web.Data.Sql.Command;

using Cloudio.Core.Models;

internal class CodeConversion : Conversion<Code, Guid>
{
    public CodeConversion() : base(e => e.Value, e => Code.CreateInstance(e))
    { }
}