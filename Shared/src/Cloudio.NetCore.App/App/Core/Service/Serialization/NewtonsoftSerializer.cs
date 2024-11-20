namespace Cloudio.Core.Services.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

public class NewtonsoftSerializer : IJsonSerializer
{
    private readonly JsonSerializerSettings _settings;

    public NewtonsoftSerializer()
    => _settings = Settings();

    public string Serialize(object obj)
    {
        var result = JsonConvert.SerializeObject(obj);
        return result;
    }

    public string Serialize<T>(T entity) where T : class
    {
        var result = JsonConvert.SerializeObject(entity, _settings);
        return result;
    }

    public T? Deserialize<T>(string value) where T : class
    {
        var result = value is { } ? JsonConvert.DeserializeObject<T>(value) : default;
        return result;
    }

    public object? Deserialize(string value, Type type)
    {
        var result = value is { } ? JsonConvert.DeserializeObject(value, type) : default;
        return result;
    }

    private static JsonSerializerSettings Settings()
    {
        var result = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        result.Converters.Add(new StringEnumConverter());

        return result;
    }
}
