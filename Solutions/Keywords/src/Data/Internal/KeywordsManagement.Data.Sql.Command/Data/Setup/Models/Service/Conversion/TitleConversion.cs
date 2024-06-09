namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloud.Web.Data.Sql.Command;
using NewsServiceTitle = Core.NewsService.Models.NewsServiceTitle;

internal class TitleConversion : Conversion<NewsServiceTitle, string>
{
    public TitleConversion() : base(e => e.Value, e => NewsServiceTitle.Instance(e))
    { }
}