namespace Cloudio.Core.Models;

public interface IEvent
{
    DateTime OccurredOn { get; }
}