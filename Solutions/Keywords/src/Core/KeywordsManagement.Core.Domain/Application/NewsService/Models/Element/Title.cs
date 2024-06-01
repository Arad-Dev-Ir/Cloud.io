namespace KeywordsManagement.Core.NewsService.Models;

using Cloud.Core;
using Cloud.Core.Models;

public class Title : Element
{
    public string Value { get; private set; } = Empty;

    #region Initialize

    private Title(string value)
    => Initialize(act: () => OnCheckTitle(value), value: value);

    void Initialize(string value, Action? act = default)
    {
        act?.Invoke();
        Value = value;
    }

    public static Title Instance(string value)
    => new(value);

    #endregion

    #region Methods

    protected override IEnumerable<object> Lookup()
    {
        yield return Value;
    }

    public static implicit operator Title(string value)
    => new(value);
    public static explicit operator string(Title title)
    => title.Value;

    public override string ToString()
    => Value;

    private void OnCheckTitle(string value)
    {
        var element = nameof(Title);
        if (value.IsEmpty())
            throw new InvalidElementException("The value for {0} cannot be null!", element);

        var minChar = 3;
        var maxChar = 50;
        if (!value.IsLengthBetween(minChar, maxChar))
            throw new InvalidElementException("The value length for {0} must be between {1} and {2} characters!", element, $"{minChar}", $"{maxChar}");
    }

    #endregion
}
