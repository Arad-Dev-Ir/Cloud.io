namespace NewsManagement.Data.Sql.News.Commands;

using Cloudio.Web.Data.Sql.Command;
using NewsManagement.Core.News.Models;

internal sealed class NewsTitleConversion : Conversion<NewsTitle, string>
{
    public NewsTitleConversion() : base(e => e.Value, e => NewsTitle.CreateInstance(e))
    { }
}