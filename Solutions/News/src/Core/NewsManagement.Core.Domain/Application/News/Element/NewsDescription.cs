namespace NewsManagement.Core.News.Models;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class NewsDescription : Element
{
    public string Value { get; }

    #region Object Instantiation

    private NewsDescription()
    { }
    private NewsDescription(string value)
    {
        ValidateNewsDescription(value);
        Value = value;
    }

    public static NewsDescription CreateInstance(string value)
    => new(value);

    #endregion

    #region Conversion Operations

    public static implicit operator NewsDescription(string value)
    => new(value);
    public static explicit operator string(NewsDescription title)
    => title.Value;

    #endregion

    #region Methods

    private void ValidateNewsDescription(string value)
    {
        var elementName = GetTypeName();

        if (value.IsEmpty())
            throw new ElementNullOrEmptyException(elementName);

        var minChars = 3;
        if (!value.IsLengthGreaterThanOrEqual(minChars))
            throw new ElementMinimumCharacterLengthException(elementName, minChars);

        var maxChars = 500;
        if (!value.IsLengthLessThanOrEqual(maxChars))
            throw new ElementMaximumCharacterLengthException(elementName, maxChars);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    => Value;

    #endregion
}