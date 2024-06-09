namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloud.Web.Data.Sql.Command;
using NewsServiceTitle = Core.NewsService.Models.NewsServiceTitle;

internal sealed class NewsServiceTitleConversion : Conversion<NewsServiceTitle, string>
{
    public NewsServiceTitleConversion() : base(e => e.Value, e => NewsServiceTitle.Instance(e))
    { }
}