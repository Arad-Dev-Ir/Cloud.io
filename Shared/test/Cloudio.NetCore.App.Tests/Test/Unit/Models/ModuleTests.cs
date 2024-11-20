namespace Cloudio.NetCore.App.Test.Models.Units;

using Cloudio.Core.Models;
using Cloudio.Web.Core;

public class ModuleTests
{
    [Fact]
    internal void Modules_of_different_types_should_not_be_equal()
    {
        // setup
        ConcreteModule moduleA = new(1);
        OtherConcreteModule moduleB = new(1);

        // exercise and verify
        (moduleA == moduleB).Should().BeFalse();
        (moduleA != moduleB).Should().BeTrue();

        moduleA.Equals(moduleB).Should().BeFalse();
        moduleB.Equals(moduleA).Should().BeFalse();

        (moduleA.GetHashCode() == moduleB.GetHashCode()).Should().BeFalse();
    }

    [Fact]
    internal void Modules_of_same_types_should_be_equal_when_ids_match()
    {
        // setup
        ConcreteModule moduleA = new(1);
        ConcreteModule moduleB = new(1);

        // exercise and verify
        (moduleA == moduleB).Should().BeTrue();
        (moduleA != moduleB).Should().BeFalse();

        moduleA.Equals(moduleB).Should().BeTrue();
        moduleB.Equals(moduleA).Should().BeTrue();

        (moduleA.GetHashCode() == moduleB.GetHashCode()).Should().BeTrue();
    }

    [Fact]
    internal void Modules_of_same_types_should_not_be_equal_when_ids_not_match()
    {
        // setup
        ConcreteModule moduleA = new(1);
        ConcreteModule moduleB = new(2);

        // exercise and verify
        (moduleA == moduleB).Should().BeFalse();
        (moduleA != moduleB).Should().BeTrue();

        moduleA.Equals(moduleB).Should().BeFalse();
        moduleB.Equals(moduleA).Should().BeFalse();

        (moduleA.GetHashCode() == moduleB.GetHashCode()).Should().BeFalse();
    }

    [Fact]
    internal void Events_should_be_empty_at_init()
    {
        // setup
        ConcreteModule module = new(1);

        // exercise and verify
        module.Events.Should().BeEmpty();
    }

    [Fact]
    internal void Events_should_have_item_when_adding_event()
    {
        // setup
        ConcreteModule module = new(1);

        // exercise
        module.RaiseEvent();

        // verify
        module.Events.Should().HaveCount(1);
    }

    [Fact]
    internal void Events_should_be_empty_when_clean_up()
    {
        // setup
        ConcreteModule module = new(1);
        module.RaiseEvent();

        // exercise
        module.ClearEvents();

        // verify
        module.Events.Should().BeEmpty();
    }

    [Fact]
    internal void Events_should_be_older_than_now()
    {
        // setup
        ConcreteModule module = new(1);
        module.RaiseEvent();

        // exercise and verify
        module.Events.First().OccurredOn.Should().BeBefore(DateTime.UtcNow);
    }

    #region Configs

    class ConcreteModule : Module
    {
        internal ConcreteModule(long id)
        => Id = Id.CreateInstance(id);

        internal void RaiseEvent() => AddEvent(new CreateModuleEvent());
    }
    class OtherConcreteModule : Module
    {
        internal OtherConcreteModule(long id)
        => Id = Id.CreateInstance(id);
    }
    record CreateModuleEvent : Event;

    #endregion
}