namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Core.NewsService.Models;

internal sealed class NewsServiceTitleConversion : Conversion<NewsServiceTitle, string>
{
    public NewsServiceTitleConversion() : base(e => e.Value, e => NewsServiceTitle.CreateInstance(e))
    { }
}