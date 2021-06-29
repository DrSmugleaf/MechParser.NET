using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MechParser.NET.Json
{
    public class EnumNameConverter<T> : JsonConverter<T> where T : Enum
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString() is not { } str)
            {
                return default;
            }

            return (T?) Enum.Parse(typeToConvert, str, true);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
