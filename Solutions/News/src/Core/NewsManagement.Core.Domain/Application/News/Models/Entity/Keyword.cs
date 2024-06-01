namespace NewsManagement.Core.News.Models;

using Cloud.Core.Models;

public class Keyword : Entity
{
    public Code KeywordCode { get; private set; }

    public Keyword(Code code)
    => Initialize(code);

    private void Initialize(Code code, Action? act = default)
    {
        act?.Invoke();
        KeywordCode = code;
    }

    public static Keyword Instance(Code code)
    => new(code);
    public static Keyword Instance(string code)
    => new(code);
}