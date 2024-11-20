namespace KeywordsManagement.Core.NewsService.Models;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class NewsServiceName : Element
{
    public string Value { get; }

    #region Object Instantiaton

    private NewsServiceName()
    { }
    private NewsServiceName(string value)
    {
        ValidateNewsServiceName(value);
        Value = value;
    }

    public static NewsServiceName CreateInstance(string value)
    => new(value);

    #endregion

    #region Conversion Operations

    public static implicit operator NewsServiceName(string value)
    => new(value);
    public static explicit operator string(NewsServiceName name)
    => name.Value;

    #endregion

    #region Methods

    private void ValidateNewsServiceName(string value)
    {
        var elementName = GetTypeName();

        if (value.IsEmpty())
            throw new ElementNullOrEmptyException(elementName);

        var minChars = 3;
        if (!value.IsLengthGreaterThanOrEqual(minChars))
            throw new ElementMinimumCharacterLengthException(elementName, minChars);

        var maxChars = 50;
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