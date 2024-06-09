namespace NewsManagement.Core.News.Models;

using Cloud.Core;
using Cloud.Core.Models;

public sealed record NewsDescription : Element
{
    public string Value { get; }

    #region Initialize

    private NewsDescription()
    { }
    private NewsDescription(string value)
    {
        OnCheckDescription(value);
        Value = value;
    }

    public static NewsDescription Instance(string value)
    => new(value);

    #endregion

    #region Conversion operators

    public static implicit operator NewsDescription(string value)
    => new(value);
    public static explicit operator string(NewsDescription title)
    => title.Value;

    #endregion

    #region Methods

    private void OnCheckDescription(string value)
    {
        var element = nameof(NewsDescription);
        if (value.IsEmpty())
            throw new InvalidElementException("The value for {0} cannot be null!", element);

        var minChar = 0;
        var maxChar = 500;
        if (!value.IsLengthBetween(minChar, maxChar))
            throw new InvalidElementException("The value length for {0} must be between {1} and {2} characters!", element, $"{minChar}", $"{maxChar}");
    }

    public override string ToString()
    => Value;

    #endregion
}
