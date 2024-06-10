namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.Keyword.Models;

internal sealed class KeywordStateConversion : Conversion<KeywordState, string>
{
    public KeywordStateConversion() : base(e => e.Value, e => KeywordState.Instance(e))
    { }
}