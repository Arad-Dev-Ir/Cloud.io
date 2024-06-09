namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.News.Models;

internal sealed class NewsBodyConversion : Conversion<NewsBody, string>
{
    public NewsBodyConversion() : base(e => e.Value, e => NewsBody.Instance(e))
    { }
}