namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using Title = Core.News.Models.Title;

internal class TitleConversion : Conversion<Title, string>
{
    public TitleConversion() : base(e => e.Value, e => Title.Instance(e))
    { }
}