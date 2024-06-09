namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.News.Models;

internal sealed class NewsTitleConversion : Conversion<NewsTitle, string>
{
    public NewsTitleConversion() : base(e => e.Value, e => NewsTitle.Instance(e))
    { }
}