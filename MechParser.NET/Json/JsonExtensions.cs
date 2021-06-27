using System.Collections.Generic;
using System.Text.Json;
using MechParser.NET.Mechs;

namespace MechParser.NET.Json
{
    public static class JsonExtensions
    {
        public static string SerializeJson(this Mech mech, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(mech, options);
        }

        public static string SerializeJson(this IEnumerable<Mech> mech, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(mech, options);
        }

        public static Mech? DeserializeSingleMech(this string json, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<Mech>(json, options);
        }

        public static List<Mech>? DeserializeList(this string json, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<List<Mech>>(json, options);
        }
    }
}
