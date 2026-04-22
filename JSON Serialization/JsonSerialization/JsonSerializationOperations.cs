using System.Text.Json;
using System.Text.Json.Serialization;

[assembly: CLSCompliant(true)]

namespace JsonSerialization;

public static class JsonSerializationOperations
{
    public static string SerializeObjectToJson(object obj)
    {
        var result = JsonSerializer.Serialize(obj);
        return result;
    }

    public static T? DeserializeJsonToObject<T>(string json)
    {
        T? result = JsonSerializer.Deserialize<T>(json);
        return result;
    }

    public static string SerializeCompanyObjectToJson(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var result = JsonSerializer.Serialize(obj);
        return result;
    }

    public static T? DeserializeCompanyJsonToObject<T>(string json)
    {
        T? result = JsonSerializer.Deserialize<T>(json);
        return result;
    }

    public static string SerializeDictionary(Company obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var options = new JsonSerializerOptions
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        };
        var result = JsonSerializer.Serialize(obj.Domains, options);

        return result;
    }

    public static string SerializeEnum(Company obj)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            },
        };
        var result = JsonSerializer.Serialize(obj, options);

        return result;
    }
}
