namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Core.NewsService.Models;

public sealed class NewsServiceNameConversion : Conversion<NewsServiceName, string>
{
    public NewsServiceNameConversion() : base(e => e.Value, e => NewsServiceName.CreateInstance(e))
    { }
}