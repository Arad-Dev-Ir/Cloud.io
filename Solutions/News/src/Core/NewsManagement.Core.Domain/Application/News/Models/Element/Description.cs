namespace NewsManagement.Core.News.Models;

using Cloud.Core;
using Cloud.Core.Models;

public record Description : Element
{
    public string Value { get; private set; }

    #region Initialize

    private Description(string value)
    => Initialize(act: () => OnCheckDescription(value), value: value);

    private void Initialize(string value, Action? act = default)
    {
        act?.Invoke();
        Value = value;
    }

    public static Description Instance(string value)
    => new(value);

    #endregion

    #region Methods

    public static implicit operator Description(string value)
    => new(value);
    public static explicit operator string(Description Description)
    => Description.Value;

    public override string ToString()
    => Value;

    private void OnCheckDescription(string value)
    {
        var element = nameof(Description);
        if (value.IsEmpty())
            throw new InvalidElementException("The value for {0} cannot be null!", element);

        var minChar = 0;
        var maxChar = 500;
        if (!value.IsLengthBetween(minChar, maxChar))
            throw new InvalidElementException("The value length for {0} must be between {1} and {2} characters!", element, $"{minChar}", $"{maxChar}");
    }

    #endregion
}
