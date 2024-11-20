namespace Cloudio.Web.Core.Contract;

public interface IRequestHandler<TInput> where TInput : IRequest
{
    Task HandleAsync(TInput input, CancellationToken token);
}

public interface IRequestHandler<TInput, TOutput> where TInput : IRequest<TOutput>
{
    Task<TOutput> HandleAsync(TInput input, CancellationToken token);
}