namespace Cloudio.Core.Models;

public class ElementInvalidException(string elementName, string value) : AppDomainException(Note.FormatByArguments(elementName, value))
{
    private const string Note = "{0} with value '{1}' is not valid";
}