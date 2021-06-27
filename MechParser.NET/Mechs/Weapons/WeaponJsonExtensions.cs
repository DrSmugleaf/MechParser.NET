using System.Collections.Generic;
using System.Text.Json;

namespace MechParser.NET.Mechs.Weapons
{
    public static class WeaponJsonExtensions
    {
        public static string SerializeJson(this Weapon weapon, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(weapon, options);
        }

        public static string SerializeJson(this IEnumerable<Weapon> mech, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(mech, options);
        }

        public static Weapon? DeserializeWeapon(this string json, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<Weapon>(json, options);
        }

        public static List<Weapon>? DeserializeWeaponList(this string json, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<List<Weapon>>(json, options);
        }
    }
}
