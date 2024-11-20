namespace Cloudio.Web.Core;

using Cloudio.Core.Models;

public interface IModule
{
    IReadOnlyCollection<IEvent> Events { get; }

    void ClearEvents();
}
