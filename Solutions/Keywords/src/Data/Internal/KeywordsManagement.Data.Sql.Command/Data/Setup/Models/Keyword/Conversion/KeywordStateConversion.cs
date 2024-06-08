namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloud.Web.Data.Sql.Command;
using KeywordState = Core.Keyword.Models.KeywordState;

internal class KeywordStateConversion : Conversion<KeywordState, string>
{
    public KeywordStateConversion() : base(e => e.Value, e => new(e))
    { }
}