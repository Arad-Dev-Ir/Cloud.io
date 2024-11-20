namespace KeywordsManagement.Test.News.Units;

using Cloudio.Core.Models;
using KeywordsManagement.Core.Keyword.Models;

public class KeywordTitleTests
{
    [Fact]
    internal void Should_create_new_keyword_title_when_calling_create_instance_method()
    {
        // setup
        var title = GetRandomWords(10);
        var keywordTitle = KeywordTitle.CreateInstance(title);

        // verify
        keywordTitle.Value.Should().Be(title);
    }

    [Fact]
    internal void Should_throw_exception_when_title_value_is_null_or_whitespace()
    {
        // exercise
        var nullAct = () => KeywordTitle.CreateInstance(null!);
        var whitespaceAct = () => KeywordTitle.CreateInstance("");

        // verify
        nullAct.Should().ThrowExactly<ElementNullOrEmptyException>();
        whitespaceAct.Should().ThrowExactly<ElementNullOrEmptyException>();
    }

    [Theory]
    [InlineData(50)]
    internal void Should_throw_exception_when_length_of_title_value_is_greater_than(int length)
    {
        // setup
        var title = GetRandomWords(length + 1);

        // exercise
        var act = () => KeywordTitle.CreateInstance(title);

        // verify
        act.Should().ThrowExactly<ElementMaximumCharacterLengthException>();
    }

    [Theory]
    [InlineData(3)]
    internal void Should_throw_exception_when_length_of_title_value_is_less_than(int length)
    {
        // setup
        var title = GetRandomWords(length - 1);

        // exercise
        var act = () => KeywordTitle.CreateInstance(title);

        // verify
        act.Should().ThrowExactly<ElementMinimumCharacterLengthException>();
    }

    #region Private Methods

    private static string GetRandomWords(int wordCount)
    {
        const string @char = "A";
        var result = string.Join(null, Enumerable.Repeat(@char, wordCount))!;
        return result;
    }

    #endregion
}