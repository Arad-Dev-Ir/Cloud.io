namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.News.Models;

internal sealed class NewsDescriptionConversion : Conversion<NewsDescription, string>
{
    public NewsDescriptionConversion() : base(e => e.Value, e => NewsDescription.Instance(e))
    { }
}