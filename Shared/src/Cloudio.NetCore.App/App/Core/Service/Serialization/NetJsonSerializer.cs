namespace Cloudio.Core.Services.Serialization;

using System;
using NetJSON;

public class NetJsonSerializer : IJsonSerializer
{
    private readonly NetJSONSettings _settings;

    public NetJsonSerializer()
    => _settings = Settings();

    public string Serialize<T>(T entity) where T : class
    {
        var result = NetJSON.Serialize<T>(entity, _settings);
        return result;
    }

    public string Serialize(object obj)
    {
        var result = obj is { } ? NetJSON.Serialize(obj, _settings) : default!;
        return result;
    }

    public T Deserialize<T>(string value) where T : class
    {
        var result = NetJSON.Deserialize<T>(value, _settings);
        return result;
    }

    public object Deserialize(string value, Type type)
    {
        var result = value is { } ? NetJSON.Deserialize(type, value, _settings) : default!;
        return result;
    }

    private static NetJSONSettings Settings()
    {
        var result = new NetJSONSettings()
        {
            //Format = NetJSONFormat.Prettify,
            UseEnumString = true,
        };

        return result;
    }
}