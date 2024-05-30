namespace Cloud.Web.Core.Contract;

public interface IAppServiceResponse
{
    ServiceStatus Status { get; set; }
    IEnumerable<string> Messages { get; }
}

public abstract class AppServiceResponse : IAppServiceResponse
{
    public ServiceStatus Status { get; set; }

    protected readonly List<string> messages = [];
    public IEnumerable<string> Messages => messages;

    public void AddMessage(string error)
    => messages.Add(error);

    public void ClearMessage()
    => messages.Clear();

    public void AddRange(IEnumerable<string> errors)
    => messages.AddRange(errors);
}