namespace Cloudio.Web.Core.AppService;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class NoHandlerExistsException(string commandName) : AppException(Note.FormatByArguments(commandName))
{
    private const string Note = "No service handler for type '{0}' has been registered";
}