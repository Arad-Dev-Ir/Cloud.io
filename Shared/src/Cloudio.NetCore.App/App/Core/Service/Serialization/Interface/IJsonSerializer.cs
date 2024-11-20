namespace Cloudio.Core.Services.Serialization;

public interface IJsonSerializer
{
    string Serialize(object obj);

    string Serialize<TObject>(TObject @object) where TObject : class;

    TObject? Deserialize<TObject>(string value) where TObject : class;

    object? Deserialize(string value, Type type);
}