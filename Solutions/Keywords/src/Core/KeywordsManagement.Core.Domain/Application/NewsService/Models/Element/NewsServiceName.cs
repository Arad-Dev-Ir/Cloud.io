﻿namespace KeywordsManagement.Core.NewsService.Models;

using Cloud.Core;
using Cloud.Core.Models;

public sealed record NewsServiceName : Element
{
    public string Value { get; }

    #region Initialize

    private NewsServiceName()
    { }
    private NewsServiceName(string value)
    {
        OnCheckName(value);
        Value = value;
    }

    public static NewsServiceName Instance(string value)
    => new(value);

    #endregion

    #region Conversion operators

    public static implicit operator NewsServiceName(string value)
    => new(value);
    public static explicit operator string(NewsServiceName name)
    => name.Value;

    #endregion

    #region Methods

    private void OnCheckName(string value)
    {
        var element = nameof(NewsServiceName);
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