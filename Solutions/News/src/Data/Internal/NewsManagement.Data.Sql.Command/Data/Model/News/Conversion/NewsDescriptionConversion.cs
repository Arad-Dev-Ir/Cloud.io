namespace NewsManagement.Data.Sql.News.Commands;

using Cloudio.Web.Data.Sql.Command;
using NewsManagement.Core.News.Models;

internal sealed class NewsDescriptionConversion : Conversion<NewsDescription, string>
{
    public NewsDescriptionConversion() : base(e => e.Value, e => NewsDescription.CreateInstance(e))
    { }
}