namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using NewsTitle = Core.News.Models.NewsTitle;

internal class TitleConversion : Conversion<NewsTitle, string>
{
    public TitleConversion() : base(e => e.Value, e => NewsTitle.Instance(e))
    { }
}