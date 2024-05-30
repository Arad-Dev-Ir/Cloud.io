namespace Cloud.Core.Extensions.Serialization;

using Microsoft.Extensions.DependencyInjection;

public static partial class Extension
{
    public static IServiceCollection AddMicrosoftSerializer(this IServiceCollection source)
    => source.AddSingleton<IJsonSerializer, MicrosoftSerializer>();

    public static IServiceCollection AddNewtonsoftSerializer(this IServiceCollection source)
    => source.AddSingleton<IJsonSerializer, NewtonsoftSerializer>();
}