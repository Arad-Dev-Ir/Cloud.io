namespace Cloudio.Core.Models;

public abstract class Entity : Model
{
    public Id Id { get; protected set; } = default!;
    public Code Code { get; protected set; } = Code.CreateInstance(UniqueIdentifier.GetId());

    protected Entity() { }

    public override bool Equals(object? obj)
    {
        var result = obj is { }
        && obj is Entity entity
        && obj.GetType() == GetType()
        && Id.Equals(entity.Id);

        return result;
    }

    public static bool operator ==(Entity left, Entity right)
    {
        var result = left is null ? right is null : left.Equals(right);
        return result;
    }

    public static bool operator !=(Entity left, Entity right)
    {
        var result = !(left == right);
        return result;
    }

    public override int GetHashCode()
    => HashCode.Combine(GetType(), Id);
}