namespace Cloudio.Web.Core.Contract;

using Cloudio.Core.Models;

public interface IEventHandler<TInput> where TInput : IEvent
{
    Task HandleAsync(TInput input, CancellationToken token);
}