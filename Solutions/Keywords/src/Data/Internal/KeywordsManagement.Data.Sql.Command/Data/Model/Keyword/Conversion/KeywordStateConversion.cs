namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Core.Keyword.Models;

internal sealed class KeywordStateConversion : Conversion<KeywordState, string>
{
    public KeywordStateConversion() : base(e => e.Value, e => KeywordState.CreateInstance(e))
    { }
}