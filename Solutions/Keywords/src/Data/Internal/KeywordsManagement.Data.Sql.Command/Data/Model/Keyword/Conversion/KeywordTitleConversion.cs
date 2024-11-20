namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Core.Keyword.Models;

internal sealed class KeywordTitleConversion : Conversion<KeywordTitle, string>
{
    public KeywordTitleConversion() : base(e => e.Value, e => KeywordTitle.CreateInstance(e))
    { }
}