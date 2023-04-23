using System.Text.Json;
using System.Text.Json.Serialization;

namespace Realtea.App.JsonConverters
{
    public class EnumConverter<T> : JsonConverter<T> where T : Enum
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string enumString = reader.GetString();

            var value = (T)Enum.Parse(typeof(T), enumString, ignoreCase: true);

            return value;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
