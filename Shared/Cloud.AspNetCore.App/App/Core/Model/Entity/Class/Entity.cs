namespace Cloud.Core.Models;

public abstract class Entity : Model
{
    public Id Id { get; protected set; }
    public Code Code { get; protected set; } = Code.Instance(Guid.NewGuid());

    protected Entity()
    { }

    public override bool Equals(object? obj)
    => (obj is Entity entity) && Id == entity.Id;

    public static bool operator ==(Entity a, Entity b)
    => OnEqual(a, null) ? OnEqual(b, null) : a.Equals(b);

    public static bool operator !=(Entity a, Entity b)
    => !(a == b);

    public override int GetHashCode()
    => Id.GetHashCode();
}