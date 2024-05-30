namespace Cloud.Core.Extensions.Serialization;

using System.Text.Json;
using Cloud.Core.Models;

public class MicrosoftSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
    public T Deserialize<T>(string value) where T : Model
    => value.IsNotEmpty() ? JsonSerializer.Deserialize<T>(value, options) : default;

    public object Deserialize(string value, Type type)
    => value.IsNotEmpty() ? JsonSerializer.Deserialize(value, type, options) : default;

    public string Serialize(object obj)
    => obj is null ? Atom.Empty : JsonSerializer.Serialize(obj, options);

    public string Serialize<T>(T entity) where T : Model
    => entity is null ? Atom.Empty : JsonSerializer.Serialize(entity, options);
}