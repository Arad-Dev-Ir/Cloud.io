namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using NewsBody = Core.News.Models.NewsBody;

internal class BodyConversion : Conversion<NewsBody, string>
{
    public BodyConversion() : base(e => e.Value, e => NewsBody.Instance(e))
    { }
}