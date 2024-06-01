namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloud.Web.Data.Sql.Command;
using Title = Core.Keyword.Models.Title;

internal class TitleConversion : Conversion<Title, string>
{
    public TitleConversion() : base(e => e.Value, e => Title.Instance(e))
    { }
}