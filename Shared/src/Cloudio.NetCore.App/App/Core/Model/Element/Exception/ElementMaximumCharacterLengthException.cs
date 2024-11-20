namespace Cloudio.Core.Models;

public class ElementMaximumCharacterLengthException(string elementName, int maxChars) :
    AppDomainException(Note.FormatByArguments(elementName, maxChars))
{
    private const string Note = "The length of {0} must be less than {1} characters";
}