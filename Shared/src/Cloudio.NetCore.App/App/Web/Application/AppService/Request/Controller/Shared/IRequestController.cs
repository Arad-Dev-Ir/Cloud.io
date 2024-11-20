namespace Cloudio.Web.Core.AppService;

using System.Threading;
using Cloudio.Web.Core.Contract;

public interface IRequestController
{
    Task PublishAsync<TInput>(TInput input, CancellationToken token) where TInput : IRequest;

    Task<TOutput> PublishAsync<TInput, TOutput>(TInput input, CancellationToken token) where TInput : IRequest<TOutput>;

    //void SetNext(IRequestController next);
}