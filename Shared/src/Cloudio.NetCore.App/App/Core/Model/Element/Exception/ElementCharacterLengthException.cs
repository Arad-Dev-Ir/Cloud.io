namespace Cloudio.Core.Models;

public class ElementCharacterLengthException(string elementName, int minChar, int maxChar) :
    AppDomainException(Note.FormatByArguments(elementName, minChar, maxChar))
{
    private const string Note = "The length of {0} must be between {1} and {2} characters";
}