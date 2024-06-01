namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloud.Web.Data.Sql.Command;
using Title = Core.NewsService.Models.Title;

internal class TitleConversion : Conversion<Title, string>
{
    public TitleConversion() : base(e => e.Value, e => Title.Instance(e))
    { }
}