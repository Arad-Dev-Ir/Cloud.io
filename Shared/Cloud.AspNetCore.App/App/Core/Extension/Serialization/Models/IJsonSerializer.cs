namespace Cloud.Core.Extensions.Serialization;

using Cloud.Core.Models;

public enum SerializationMethod : byte
{
    Microsoft = 1,
    Newtonsoft
}

public interface IJsonSerializer
{
    string Serialize(object obj);
    string Serialize<T>(T entity) where T : Model;
    T Deserialize<T>(string value) where T : Model;
    object Deserialize(string value, Type type);
}