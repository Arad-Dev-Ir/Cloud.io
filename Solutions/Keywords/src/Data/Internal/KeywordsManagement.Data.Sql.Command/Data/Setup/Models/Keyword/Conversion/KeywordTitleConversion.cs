namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.Keyword.Models;

internal sealed class KeywordTitleConversion : Conversion<KeywordTitle, string>
{
    public KeywordTitleConversion() : base(e => e.Value, e => KeywordTitle.Instance(e))
    { }
}