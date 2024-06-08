namespace NewsManagement.Core.News.Models;

using Cloud.Core;
using Cloud.Core.Models;

public record Body : Element
{
    public string Value { get; }

    #region Initialize

    private Body()
    { }
    private Body(string value)
    {
        OnCheckTitle(value);
        Value = value;
    }

    public static Body Instance(string value)
    => new(value);

    #endregion

    #region Conversion operators

    public static implicit operator Body(string value)
    => new(value);
    public static explicit operator string(Body title)
    => title.Value;

    #endregion

    #region Methods

    private void OnCheckTitle(string value)
    {
        var element = nameof(Body);
        if (value.IsEmpty())
            throw new InvalidElementException("The value for {0} cannot be null!", element);

        var minChar = 2;
        var maxChar = 250;
        if (!value.IsLengthBetween(minChar, maxChar))
            throw new InvalidElementException("The value length for {0} must be between {1} and {2} characters!", element, $"{minChar}", $"{maxChar}");
    }

    public override string ToString()
    => Value;

    #endregion
}