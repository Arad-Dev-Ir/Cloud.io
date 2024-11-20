namespace Cloudio.Core.Models;

public abstract class Model : Atom
{
    public Model() { }

    protected string GetTypeName()
    => GetType().Name;
}