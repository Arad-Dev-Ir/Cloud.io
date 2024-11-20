namespace NewsManagement.Data.Sql.News.Commands;

using Cloudio.Web.Data.Sql.Command;
using NewsManagement.Core.News.Models;

internal sealed class NewsBodyConversion : Conversion<NewsBody, string>
{
    public NewsBodyConversion() : base(e => e.Value, e => NewsBody.CreateInstance(e))
    { }
}