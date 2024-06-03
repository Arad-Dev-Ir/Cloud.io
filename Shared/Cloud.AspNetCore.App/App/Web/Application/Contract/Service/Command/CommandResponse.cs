namespace Cloud.Web.Core.Contract;

public class CommandResponse : AppServiceResponse
{ }

public sealed class CommandResponse<D> : CommandResponse, ICommandResponse<D>
{
    public D Data { get; set; }
}