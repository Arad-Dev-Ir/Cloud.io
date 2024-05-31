namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloud.Web.Data.Sql.Command;
using Mode = Core.Keyword.Models.Mode;

internal class ModeConversion : Conversion<Mode, string>
{
    public ModeConversion() : base(e => e.Value, e => new(e))
    { }
}