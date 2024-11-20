namespace Cloudio.Core.Models;

public abstract class Element : Model
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        var result = obj is { } &&
        obj is Element element &&
        obj.GetType() == GetType() &&
        GetEqualityComponents().SequenceEqual(element.GetEqualityComponents());

        return result;
    }

    public static bool operator ==(Element left, Element right)
    => left.Equals(right);

    public static bool operator !=(Element left, Element right)
    => !left.Equals(right);

    public override int GetHashCode()
    {
        var result = GetEqualityComponents()
        .Select(x => x?.GetHashCode() ?? 0)
        .Aggregate((x, y) => x ^ y);

        return result;
    }
}