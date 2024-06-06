namespace Cloud.Core.Models;

public abstract record Element : TransferModel;

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