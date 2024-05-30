namespace Cloud.Core.Models;

public class Id : Element
{
    public long Value { get; private set; }

    private Id() { }
    private Id(long value)
    => Value = value;

    private void Initialize(Action? action = default)
    { }

    public static Id Instance(long value) => new(value);

    #region Override Methods

    protected override IEnumerable<object> Lookup()
    {
        yield return Value;
    }
    public override string ToString() => Value.ToString();

    #endregion

    #region Implicit and Explicit Casting

    public static implicit operator Id(long id) => Instance(id);
    public static explicit operator long(Id code) => code.Value;

    #endregion
}