namespace Cloud.Core.Models;

using System.Reflection;

public abstract class Model : Atom
{
    public Model() { }

    protected object? CallMethod(string name, Type type, object?[] parameters)
    {
        var method = GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic, [type]);
        var result = method?.Invoke(this, parameters);
        return result;
    }

    protected static bool OnEqual(object? a, object? b)
    => ReferenceEquals(a, b);
}


public abstract record Record() : Object;