namespace NewsManagement.Data.External.Messaging;

using Microsoft.Extensions.DependencyInjection;
using Cloudio.Core.Services.Messaging;

public static class Extensions
{
    public static IServiceCollection ConfigureRabbitMessageConsumer(this IServiceCollection services, Action<MessageQueueConfigs> act)
    => services.AddSingleton<IMessageConsumer, MessageConsumer>().Configure(act);

    public static IServiceCollection ConfigureKeywordsEventConsumer(this IServiceCollection services, Action<DatabaseConfigs> act)
    => services.AddSingleton<KeywordsEventConsumer>().Configure(act);

    public static IServiceCollection ConfigureEventConsumerHostedService(this IServiceCollection services)
    => services.AddHostedService<EventConsumerHostedService>();
}