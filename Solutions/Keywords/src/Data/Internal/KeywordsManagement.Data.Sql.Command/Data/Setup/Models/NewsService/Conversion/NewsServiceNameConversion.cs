namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloud.Web.Data.Sql.Command;
using NewsServiceName = Core.NewsService.Models.NewsServiceName;

public sealed class NewsServiceNameConversion : Conversion<NewsServiceName, string>
{
    public NewsServiceNameConversion() : base(e => e.Value, e => NewsServiceName.Instance(e))
    { }
}