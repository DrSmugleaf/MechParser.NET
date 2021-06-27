using System.Collections.Generic;
using System.Text.Json;

namespace MechParser.NET.Mechs
{
    public static class MechJsonExtensions
    {
        public static string SerializeJson(this Mech mech, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(mech, options);
        }

        public static string SerializeJson(this IEnumerable<Mech> mech, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(mech, options);
        }

        public static Mech? DeserializeMech(this string json, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<Mech>(json, options);
        }

        public static List<Mech>? DeserializeMechList(this string json, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<List<Mech>>(json, options);
        }
    }
}
