namespace Cloudio.Core.Models;

using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public abstract record Enumer : DataTransferObject
{
    protected Enumer(string value)
     => Value = value.IsEmpty() ? Atom.Empty : value;

    public string Value { get; private set; }
    public abstract string Display { get; }

    public static List<T> GetItems<T>() where T : Enumer
    {
        var result = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static)
        .First(e => e.Name == "Items")
        .GetValue(null)?
        .As<IList>()
        .Cast<T>()
        .ToList();

        return result!;
    }

    #region Equality

    //public override bool Equals(object? obj)
    //{
    //    var b = obj as Enumer;
    //    var result = b.As<object> != null ? Value == b.Value : false;
    //    return result;
    //}

    //public static bool operator ==(Enumer a, Enumer b)
    //=> a.As<object> != null ? a.Equals(b) : false;

    //public static bool operator !=(Enumer a, Enumer b)
    //=> !(a == b);

    //public int Compare(object? x, object? y)
    //=> Value.CompareTo(((Enumer)y).Value);

    //public override int GetHashCode()
    //=> base.GetHashCode();

    #endregion
}