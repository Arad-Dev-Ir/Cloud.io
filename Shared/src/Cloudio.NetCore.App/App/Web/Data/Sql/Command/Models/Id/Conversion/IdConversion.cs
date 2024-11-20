namespace Cloudio.Web.Data.Sql.Command;

using Cloudio.Core.Models;

internal class IdConversion : Conversion<Id, long>
{
    public IdConversion() : base(e => e.Value, e => Id.CreateInstance(e)) { }
}