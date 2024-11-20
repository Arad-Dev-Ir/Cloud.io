namespace Cloudio.Web.Core.AppService;

using System;
using System.Threading;
using Cloudio.Web.Core.Contract;

public abstract class AbstractRequestController(IServiceProvider serviceProvider) : IRequestController
{
    protected readonly IServiceProvider ServiceProvider = serviceProvider;
    protected IRequestController? Next { get; private set; }

    public abstract Task PublishAsync<TInput>(TInput input, CancellationToken token) where TInput : IRequest;

    public abstract Task<TOutput> PublishAsync<TInput, TOutput>(TInput input, CancellationToken token) where TInput : IRequest<TOutput>;

    public void SetNext(IRequestController next)
    => Next = next;
}