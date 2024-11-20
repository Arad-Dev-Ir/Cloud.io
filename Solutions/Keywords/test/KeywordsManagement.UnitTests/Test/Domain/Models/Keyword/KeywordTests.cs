namespace KeywordsManagement.Test.News.Units;

using KeywordsManagement.Core.Keyword.Models;

public class KeywordTests
{
    [Fact]
    internal void Should_create_keyword_when_calling_create_instance_method()
    {
        // setup
        var title = KeywordTitle.CreateInstance("new-keyword");
        var keyword = Keyword.CreateInstance(title);

        // verify
        keyword.Title.Should().Be(title);
    }

    [Fact]
    internal void Should_set_keyword_state_to_preview_when_a_new_keyword_is_created()
    {
        // setup
        var title = KeywordTitle.CreateInstance("new-keyword");
        var keyword = Keyword.CreateInstance(title);

        // verify
        keyword.State.Should().Be(KeywordState.Preview);
    }

    [Fact]
    internal void Should_raise_event_when_a_new_keyword_is_created()
    {
        // setup
        var title = KeywordTitle.CreateInstance("new-keyword");

        // exercise
        var keyword = Keyword.CreateInstance(title);

        // verify
        keyword.Events.Should().ContainItemsAssignableTo<KeywordCreated>();
    }

    [Fact]
    internal void Should_change_keyword_title_when_calling_change_title_method()
    {
        // setup
        var oldTitle = KeywordTitle.CreateInstance("old-keyword");
        var keyword = Keyword.CreateInstance(oldTitle);

        var newTitle = KeywordTitle.CreateInstance("new-keyword");

        // exercise
        keyword.ChangeTitle(newTitle);

        // verify
        keyword.Title.Should().NotBe(oldTitle);
        keyword.Title.Should().Be(newTitle);
    }

    [Fact]
    internal void Should_set_keyword_state_to_preview_when_calling_change_title_method()
    {
        // setup
        var oldTitle = KeywordTitle.CreateInstance("old-keyword");
        var keyword = Keyword.CreateInstance(oldTitle);

        var newTitle = KeywordTitle.CreateInstance("new-keyword");

        // exercise
        keyword.ChangeTitle(newTitle);

        // verify
        keyword.State.Should().Be(KeywordState.Preview);
    }

    [Fact]
    internal void Should_throw_exception_when_keyword_is_already_inactive()
    {
        // setup
        var oldTitle = KeywordTitle.CreateInstance("old-keyword");
        var keyword = Keyword.CreateInstance(oldTitle);

        keyword.Deactivate();
        var newTitle = KeywordTitle.CreateInstance("new-keyword");

        // exercise
        var act = () => keyword.ChangeTitle(newTitle);

        // verify
        act.Should().ThrowExactly<ChangeKeywordTitleException>();
    }

    [Fact]
    internal void Should_activate_keyword_when_calling_activate_method()
    {
        // setup
        var title = KeywordTitle.CreateInstance("new-keyword");
        var keyword = Keyword.CreateInstance(title);

        // exercise
        keyword.Activate();

        // verify
        keyword.State.Should().Be(KeywordState.Active);
    }

    [Fact]
    internal void Should_raise_event_when_calling_activate_method()
    {
        // setup
        var title = KeywordTitle.CreateInstance("new-keyword");
        var keyword = Keyword.CreateInstance(title);

        // exercise
        keyword.Activate();

        // verify
        keyword.Events.Should().ContainItemsAssignableTo<KeywordCreated>();
        keyword.Events.Should().ContainItemsAssignableTo<KeywordActivated>();
    }

    [Fact]
    internal void Should_throw_exception_when_calling_activate_method_again_whereas_keyword_is_already_active()
    {
        // setup
        var title = KeywordTitle.CreateInstance("new-keyword");
        var keyword = Keyword.CreateInstance(title);
        keyword.Activate();

        // exercise
        var act = () => keyword.Activate();

        // verify
        act.Should().ThrowExactly<KeywordIsAlreadyActiveException>();
    }

    [Fact]
    internal void Should_deactivate_keyword_when_calling_dectivate_method()
    {
        // setup
        var title = KeywordTitle.CreateInstance("new-keyword");
        var keyword = Keyword.CreateInstance(title);

        // exercise
        keyword.Deactivate();

        // verify
        keyword.State.Should().Be(KeywordState.Inactive);
    }

    [Fact]
    internal void Should_throw_exception_when_calling_deactivate_method_again_whereas_keyword_is_already_inactive()
    {
        // setup
        var title = KeywordTitle.CreateInstance("new-keyword");
        var keyword = Keyword.CreateInstance(title);
        keyword.Deactivate();

        // exercise
        var act = () => keyword.Deactivate();

        // verify
        act.Should().ThrowExactly<KeywordIsAlreadyInactiveException>();
    }
}