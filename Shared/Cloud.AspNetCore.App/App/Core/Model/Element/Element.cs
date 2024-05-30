namespace Cloud.Core.Models;

using Cloud.Core;

public abstract class Element : Model
{
    protected abstract IEnumerable<object> Lookup();

    public override bool Equals(object? obj)
    => (obj is Element b) && Lookup().SequenceEqual(b.Lookup());

    protected static bool Equal(Element a, Element b)
    {
        var result = default(bool);
        var equal = OnEqual(a, null) ^ OnEqual(b, null);
        if (!equal)
            result = OnEqual(a, b) || a.Equals(b);
        return result;
    }

    public static bool operator ==(Element a, Element b)
    => Equal(a, b);
    public static bool operator !=(Element a, Element b)
    => !(a == b);

    public override int GetHashCode()
    => Lookup()
    .Select(e => e != null ? e.GetHashCode() : 0)
    .Aggregate((o, r) => o ^ r);
}

#region Old

//public abstract class Element<E> : Model, IEquatable<E> where E : Element<E>
//{
//    public bool Equals(E? other)
//    => other != null && this == other;

//    protected abstract IEnumerable<object> Lookup();

//    public override bool Equals(object? obj)
//    => (obj is E b) && Lookup().SequenceEqual(b.Lookup());

//    public static bool operator ==(Element<E> a, Element<E> b) => ((object)a == null) ? false : a.Equals(b);
//    public static bool operator !=(Element<E> a, Element<E> b) => !(a == b);

//    public override int GetHashCode()
//    => Lookup()
//    .Select(e => e != null ? e.GetHashCode() : 0)
//    .Aggregate((o, r) => o ^ r);
//}

#endregion