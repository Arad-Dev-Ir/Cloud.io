namespace NewsManagement.Core.News.Models;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class NewsBody : Element
{
    public string Value { get; }

    #region Object Instantiation

    private NewsBody()
    { }
    private NewsBody(string value)
    {
        ValidateNewsBody(value);
        Value = value;
    }

    public static NewsBody CreateInstance(string value)
    => new(value);

    #endregion

    #region Conversion Operations

    public static implicit operator NewsBody(string value)
    => new(value);
    public static explicit operator string(NewsBody title)
    => title.Value;

    #endregion

    #region Methods

    private void ValidateNewsBody(string value)
    {
        var elementName = GetTypeName();

        if (value.IsEmpty())
            throw new ElementNullOrEmptyException(elementName);

        var minChars = 3;
        if (!value.IsLengthGreaterThanOrEqual(minChars))
            throw new ElementMinimumCharacterLengthException(elementName, minChars);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    => Value;

    #endregion
}