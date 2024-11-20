namespace Cloudio.Core.Models;

public sealed class Code : Element
{
    public Guid Value { get; }

    #region Object Instantiation

    private Code()
    { }
    private Code(Guid value)
    => Value = value;
    private Code(string value)
    {
        ValidateCode(value);
        Value = new Guid(value);
    }

    public static Code CreateInstance(Guid value) => new(value);
    public static Code CreateInstance(string value) => new(value);
    public static Code CreateInstance() => new(value: UniqueIdentifier.GetId());

    #endregion

    #region Conversion Operations

    public static implicit operator Code(Guid id) => CreateInstance(id);
    public static implicit operator Code(string id) => CreateInstance(id);

    public static explicit operator Guid(Code code) => code.Value;
    public static explicit operator string(Code code) => code.ToString();

    #endregion

    #region Methods

    private static void ValidateCode(string value)
    {
        if (value.IsEmpty())
            throw new ElementNullOrEmptyException(nameof(Code));

        if (!Guid.TryParse(value, out _))
            throw new ElementInvalidException(nameof(Code), value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    => Value.ToString();

    #endregion
}