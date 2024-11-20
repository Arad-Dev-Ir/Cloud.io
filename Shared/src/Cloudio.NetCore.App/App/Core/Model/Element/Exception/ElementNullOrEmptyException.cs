namespace Cloudio.Core.Models;

public class ElementNullOrEmptyException(string elementName) : AppDomainException(Note.FormatByArguments(elementName))
{
    private const string Note = "{0} cannot be null or empty";
}