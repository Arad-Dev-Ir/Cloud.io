namespace Cloud.Web.Core.AppService;

using Cloud.Core.Models;

public abstract class Procedure<T>(IServiceProvider serviceProvider) : Model where T : Procedure<T>
{
    protected readonly IServiceProvider ServiceProvider = serviceProvider;

    protected T? Next { get; private set; }
    public T Set(T next)
    => Next = next;
}