namespace Cloudio.Core.Models;

public sealed class Id(long value) : Element
{
    public long Value { get; } = value;


    public static Id CreateInstance(long value) => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Id(long id) => new(id);
    public static explicit operator long(Id code) => code.Value;

    public override string ToString()
    => Value.ToString();
}