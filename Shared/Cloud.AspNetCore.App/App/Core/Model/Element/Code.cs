namespace Cloud.Core.Models;

public class Code : Element
{
    public Guid Value { get; private set; }

    private Code() { }
    private Code(string value)
    => Initialize(() => OnCheckCode(value));

    #region Initialize

    private void Initialize(Action? action = default)
    => action?.Invoke();

    public static Code Instance(Guid id) => new() { Value = id };
    public static Code Instance(string value) => new(value);

    #endregion

    #region Override Methods

    protected override IEnumerable<object> Lookup()
    {
        yield return Value;
    }
    public override string ToString() => Value.ToString();

    #endregion

    #region Implicit and Explicit Casting

    public static implicit operator Code(Guid id) => Instance(id);
    public static implicit operator Code(string id) => Instance(id);

    public static explicit operator Guid(Code code) => code.Value;
    public static explicit operator string(Code code) => code.ToString();

    #endregion

    #region Methods

    private void OnCheckCode(string value)
    {
        if (value.IsEmpty()) throw new InvalidElementException("", nameof(value));
        if (Guid.TryParse(value, out var codeId)) Value = codeId;
        else throw new InvalidElementException("", nameof(Code));
    }

    #endregion
}