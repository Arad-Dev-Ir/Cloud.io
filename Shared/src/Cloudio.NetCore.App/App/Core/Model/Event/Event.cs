namespace Cloudio.Core.Models;

public record Event : DataTransferObject, IEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}