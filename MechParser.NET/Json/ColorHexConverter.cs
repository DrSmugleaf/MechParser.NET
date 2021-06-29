using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MechParser.NET.Json
{
    public class ColorHexConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString() is not { } str)
            {
                return default;
            }

            return ColorTranslator.FromHtml(str);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(ColorTranslator.ToHtml(value));
        }
    }
}
