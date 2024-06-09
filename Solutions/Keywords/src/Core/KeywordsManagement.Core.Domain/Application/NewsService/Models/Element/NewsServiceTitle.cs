namespace KeywordsManagement.Core.NewsService.Models;

using Cloud.Core;
using Cloud.Core.Models;

public sealed record NewsServiceTitle : Element
{
    public string Value { get; }

    #region Initialize

    private NewsServiceTitle()
    { }
    private NewsServiceTitle(string value)
    {
        OnCheckTitle(value);
        Value = value;
    }

    public static NewsServiceTitle Instance(string value)
    => new(value);

    #endregion

    #region Conversion operators

    public static implicit operator NewsServiceTitle(string value)
    => new(value);
    public static explicit operator string(NewsServiceTitle title)
    => title.Value;

    #endregion

    #region Methods

    private void OnCheckTitle(string value)
    {
        var element = nameof(NewsServiceTitle);
        if (value.IsEmpty())
            throw new InvalidElementException("The value for {0} cannot be null!", element);

        var minChar = 3;
        var maxChar = 50;
        if (!value.IsLengthBetween(minChar, maxChar))
            throw new InvalidElementException("The value length for {0} must be between {1} and {2} characters!", element, $"{minChar}", $"{maxChar}");
    }

    public override string ToString()
    => Value;

    #endregion
}