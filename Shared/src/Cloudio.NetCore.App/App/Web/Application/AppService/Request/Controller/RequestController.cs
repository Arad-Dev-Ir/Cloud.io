namespace Cloudio.Web.Core.AppService;

using System;
using System.Threading;
using Cloudio.Web.Core.Contract;

public class RequestController(IServiceProvider serviceProvider) : AbstractRequestController(serviceProvider)
{
    public override async Task PublishAsync<TInput>(TInput input, CancellationToken token)
    {
        var handler = ServiceProvider.GetService<IRequestHandler<TInput>>();
        if (handler is null)
            throw new NoHandlerExistsException(typeof(TInput).Name);

        await handler.HandleAsync(input, token);
    }

    public override async Task<TOutput> PublishAsync<TInput, TOutput>(TInput input, CancellationToken token)
    {
        var handler = ServiceProvider.GetService<IRequestHandler<TInput, TOutput>>();
        if (handler is null)
            throw new NoHandlerExistsException(typeof(TInput).Name);

        var result = await handler.HandleAsync(input, token);
        return result;
    }
}