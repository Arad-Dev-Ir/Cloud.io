namespace Cloudio.NetCore.App.Test.Models.Units;

using Cloudio.Core.Models;

public class EntityTests
{
    [Fact]
    internal void Entities_of_different_types_should_not_be_equal()
    {
        // setup
        ConcreteEntity entityA = new(1);
        OtherConcreteEntity entityB = new(1);

        // exercise and verify
        (entityA == entityB).Should().BeFalse();
        (entityA != entityB).Should().BeTrue();

        entityA.Equals(entityB).Should().BeFalse();
        entityB.Equals(entityA).Should().BeFalse();

        (entityA.GetHashCode() == entityB.GetHashCode()).Should().BeFalse();
    }

    [Fact]
    internal void Entities_of_same_types_should_be_equal_when_ids_match()
    {
        // setup
        ConcreteEntity entityA = new(1);
        ConcreteEntity entityB = new(1);

        // exercise and verify
        (entityA == entityB).Should().BeTrue();
        (entityA != entityB).Should().BeFalse();

        entityA.Equals(entityB).Should().BeTrue();
        entityB.Equals(entityA).Should().BeTrue();

        (entityA.GetHashCode() == entityB.GetHashCode()).Should().BeTrue();
    }

    [Fact]
    internal void Entities_of_same_types_should_be_equal_when_ids_not_match()
    {
        // setup
        ConcreteEntity entityA = new(1);
        ConcreteEntity entityB = new(2);

        // exercise and verify
        (entityA == entityB).Should().BeFalse();
        (entityA != entityB).Should().BeTrue();

        entityA.Equals(entityB).Should().BeFalse();
        entityB.Equals(entityA).Should().BeFalse();

        (entityA.GetHashCode() == entityB.GetHashCode()).Should().BeFalse();
    }

    #region Configs

    class ConcreteEntity : Entity
    {
        public ConcreteEntity(long id)
        => Id = Id.CreateInstance(id);
    }
    class OtherConcreteEntity : Entity
    {
        public OtherConcreteEntity(long id)
        => Id = Id.CreateInstance(id);
    }

    #endregion
}
