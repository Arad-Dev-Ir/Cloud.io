namespace NewsManagement.Core.News.Models;

using Cloudio.Core.Models;

public sealed class Keyword : Entity
{
    public Code KeywordCode { get; private set; }

    #region Object Instantiation

    private Keyword()
    { }
    public Keyword(Code code)
    => KeywordCode = code;

    public static Keyword CreateInstance(Code code)
    => new(code);
    public static Keyword CreateInstance(string code)
    => new(code);

    #endregion
}