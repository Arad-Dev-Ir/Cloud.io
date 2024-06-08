namespace NewsManagement.Core.News.Models;

using Cloud.Core.Models;

public class Keyword : Entity
{
    public Code KeywordCode { get; private set; }

    #region Initialize

    private Keyword()
    { }
    public Keyword(Code code)
    => KeywordCode = code;

    public static Keyword Instance(Code code)
    => new(code);
    public static Keyword Instance(string code)
    => new(code);

    #endregion
}