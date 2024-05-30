namespace Cloud.Web.Data.Sql.Command;

using Cloud.Core.Models;

internal class IdConversion : Conversion<Id, long>
{
    public IdConversion() : base(e => e.Value, e => Id.Instance(e))
    { }
}