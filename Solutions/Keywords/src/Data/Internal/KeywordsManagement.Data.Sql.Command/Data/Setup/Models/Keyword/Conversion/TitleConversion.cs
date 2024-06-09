namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloud.Web.Data.Sql.Command;
using KeywordTitle = Core.Keyword.Models.KeywordTitle;

internal class TitleConversion : Conversion<KeywordTitle, string>
{
    public TitleConversion() : base(e => e.Value, e => KeywordTitle.Instance(e))
    { }
}