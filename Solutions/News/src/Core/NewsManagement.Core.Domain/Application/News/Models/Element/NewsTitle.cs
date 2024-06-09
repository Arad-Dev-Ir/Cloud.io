namespace NewsManagement.Core.News.Models;

using Cloud.Core;
using Cloud.Core.Models;

public sealed record NewsTitle : Element
{
    public string Value { get; }

    #region Initialize

    private NewsTitle()
    { }
    private NewsTitle(string value)
    {
        OnCheckTitle(value);
        Value = value;
    }

    public static NewsTitle Instance(string value)
    => new(value);

    #endregion

    #region Conversion operators

    public static implicit operator NewsTitle(string value)
    => new(value);
    public static explicit operator string(NewsTitle title)
    => title.Value;

    #endregion

    #region Methods

    private void OnCheckTitle(string value)
    {
        var element = nameof(NewsTitle);
        if (value.IsEmpty())
            throw new InvalidElementException("The value for {0} cannot be null!", element);

        var minChar = 3;
        var maxChar = 250;
        if (!value.IsLengthBetween(minChar, maxChar))
            throw new InvalidElementException("The value length for {0} must be between {1} and {2} characters!", element, $"{minChar}", $"{maxChar}");
    }

    public override string ToString()
    => Value;

    #endregion
}