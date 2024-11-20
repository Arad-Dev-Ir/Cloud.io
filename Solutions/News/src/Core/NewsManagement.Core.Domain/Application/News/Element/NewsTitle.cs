namespace NewsManagement.Core.News.Models;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class NewsTitle : Element
{
    public string Value { get; }

    #region Object Instantiation

    private NewsTitle()
    { }
    private NewsTitle(string value)
    {
        ValidateNewsTitle(value);
        Value = value;
    }

    public static NewsTitle CreateInstance(string value)
    => new(value);

    #endregion

    #region Conversion Operations

    public static implicit operator NewsTitle(string value)
    => new(value);
    public static explicit operator string(NewsTitle title)
    => title.Value;

    #endregion

    #region Methods

    private void ValidateNewsTitle(string value)
    {
        var elementName = GetTypeName();

        if (value.IsEmpty())
            throw new ElementNullOrEmptyException(elementName);

        var minChars = 3;
        if (!value.IsLengthGreaterThanOrEqual(minChars))
            throw new ElementMinimumCharacterLengthException(elementName, minChars);

        var maxChars = 250;
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