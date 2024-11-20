namespace NewsManagement.Test.News.Units;

using Cloudio.Core.Models;
using NewsManagement.Core.News.Models;

public class NewsBodyTests
{
    [Fact]
    internal void Should_create_new_keyword_title_when_calling_create_instance_method()
    {
        // setup
        var title = GetRandomWords(10);
        var keywordTitle = NewsBody.CreateInstance(title);

        // verify
        keywordTitle.Value.Should().Be(title);
    }

    [Fact]
    internal void Should_throw_exception_when_title_value_is_null_or_whitespace()
    {
        // exercise
        var nullAct = () => NewsBody.CreateInstance(null!);
        var whitespaceAct = () => NewsBody.CreateInstance("");

        // verify
        nullAct.Should().ThrowExactly<ElementNullOrEmptyException>();
        whitespaceAct.Should().ThrowExactly<ElementNullOrEmptyException>();
    }

    [Theory]
    [InlineData(3)]
    internal void Should_throw_exception_when_length_of_title_value_is_less_than(int length)
    {
        // setup
        var title = GetRandomWords(length - 1);

        // exercise
        var act = () => NewsBody.CreateInstance(title);

        // verify
        act.Should().ThrowExactly<ElementMinimumCharacterLengthException>();
    }

    #region Private Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count">Count of needed characters to create a random word</param>
    /// <returns></returns>
    private static string GetRandomWords(int count)
    {
        const string @char = "a";
        var result = string.Join(null, Enumerable.Repeat(@char, count))!;
        return result;
    }

    #endregion
}