namespace Cloud.Core.Extensions.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Cloud.Core.Models;

public class NewtonsoftSerializer : IJsonSerializer
{
    public T Deserialize<T>(string value) where T : Model
    => value.IsNotEmpty() ? JsonConvert.DeserializeObject<T>(value) : default;

    public string Serialize(object obj)
     => JsonConvert.SerializeObject(obj);

    private static readonly JsonSerializerSettings _settings = new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
    public string Serialize<T>(T entity) where T : Model
    => JsonConvert.SerializeObject(entity, _settings);

    public object Deserialize(string value, Type type)
   => value.IsNotEmpty() ? JsonConvert.DeserializeObject(value, type) : default;
}
