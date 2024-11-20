namespace Cloudio.Core.Models;

public class ElementMinimumCharacterLengthException(string elementName, int minChars) :
    AppDomainException(Note.FormatByArguments(elementName, minChars))
{
    private const string Note = "The length of {0} must be greater than {1} characters";
}