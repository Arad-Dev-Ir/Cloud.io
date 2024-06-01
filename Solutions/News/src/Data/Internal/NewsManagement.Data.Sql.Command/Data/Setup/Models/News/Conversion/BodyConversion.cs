namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using Body = Core.News.Models.Body;

internal class BodyConversion : Conversion<Body, string>
{
    public BodyConversion() : base(e => e.Value, e => Body.Instance(e))
    { }
}