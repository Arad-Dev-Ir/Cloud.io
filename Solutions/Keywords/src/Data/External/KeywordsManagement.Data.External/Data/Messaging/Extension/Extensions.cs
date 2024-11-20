namespace KeywordsManagement.Data.External.Messaging;

using Microsoft.Extensions.DependencyInjection;
using Cloudio.Core.Services.Messaging;

public static class Extensions
{
    public static IServiceCollection ConfigureMessagePublisher(this IServiceCollection serviceCollection, Action<MessageQueueConfigs> act)
    => serviceCollection.AddSingleton<IMessagePublisher, MessagePublisher>().Configure(act);

    public static IServiceCollection ConfigureKeywordsEventPublisher(this IServiceCollection serviceCollection)
    => serviceCollection.AddSingleton<KeywordsEventPublisher>();

    public static IServiceCollection ConfigureEventPublisherHostedService(this IServiceCollection serviceCollection)
   => serviceCollection.AddHostedService<EventPublisherHostedService>();
}