namespace Cloudio.Core.Services.Serialization;

public static partial class Extensions
{
    public static IServiceCollection ConfigureSerializer(this IServiceCollection services, SerializationMethod serializationMethod)
    {
        var result = serializationMethod switch
        {
            SerializationMethod.NetJson => services.AddSingleton<IJsonSerializer, NetJsonSerializer>(),
            SerializationMethod.Newtonsoft => services.AddSingleton<IJsonSerializer, NewtonsoftSerializer>(),
            SerializationMethod.Microsoft => services.AddSingleton<IJsonSerializer, MicrosoftSerializer>(),
            _ => services.AddSingleton<IJsonSerializer, NewtonsoftSerializer>()
        };

        return result;
    }
}