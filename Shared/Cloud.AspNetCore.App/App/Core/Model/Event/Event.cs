namespace Cloud.Core.Models;

public interface IEvent
{ }

public record Event : Record, IEvent
{ }