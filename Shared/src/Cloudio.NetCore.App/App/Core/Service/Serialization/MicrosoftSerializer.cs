namespace Cloudio.Core.Services.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;

public class MicrosoftSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _options;

    public MicrosoftSerializer()
    => _options = Options();

    public string Serialize<T>(T entity) where T : class
    {
        var result = entity is { } ? JsonSerializer.Serialize(entity, _options) : default!;
        return result;
    }

    public string Serialize(object obj)
    {
        var result = obj is { } ? JsonSerializer.Serialize(obj, _options) : default!;
        return result;
    }

    public T? Deserialize<T>(string value) where T : class
    {
        var result = value is { } ? JsonSerializer.Deserialize<T>(value, _options) : default;
        return result;
    }

    public object? Deserialize(string value, Type type)
    {
        var result = value is { } ? JsonSerializer.Deserialize(value, type, _options) : default;
        return result;
    }

    private static JsonSerializerOptions Options()
    {
        var result = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        result.Converters.Add(new JsonStringEnumConverter());

        return result;
    }
}