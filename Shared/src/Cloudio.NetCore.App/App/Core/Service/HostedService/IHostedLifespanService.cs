namespace Cloudio.Core;

using Microsoft.Extensions.Hosting;

public interface IHostedLifespanService : IHostedLifecycleService
{
    Task IHostedLifecycleService.StartingAsync(CancellationToken token)
    => Task.CompletedTask;

    Task IHostedService.StartAsync(CancellationToken token)
    => Task.CompletedTask;

    Task IHostedLifecycleService.StartedAsync(CancellationToken token)
     => Task.CompletedTask;

    Task IHostedLifecycleService.StoppingAsync(CancellationToken token)
   => Task.CompletedTask;

    Task IHostedService.StopAsync(CancellationToken token)
     => Task.CompletedTask;

    Task IHostedLifecycleService.StoppedAsync(CancellationToken token)
    => Task.CompletedTask;
}