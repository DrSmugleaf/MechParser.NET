using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MechParser.NET.Json;
using MechParser.NET.Mechs.Slots;

namespace MechParser.NET.Mechs.Weapons
{
    [PublicAPI]
    public record Weapon
    {
        public Weapon(
            HardpointType type,
            Faction faction,
            string name,
            double damage,
            double heat,
            double cooldown,
            int minimumRange,
            int optimalRange,
            int maximumRange,
            int slots,
            double tons,
            int? speed,
            double? ammoPerTon,
            double damagePerSecond,
            double damagePerHeat,
            double damagePerTon,
            double heatPerSecond,
            double impulse,
            double health,
            int? costs)
        {
            Type = type;
            Faction = faction;
            Name = name;
            Damage = damage;
            Heat = heat;
            Cooldown = cooldown;
            MinimumRange = minimumRange;
            OptimalRange = optimalRange;
            MaximumRange = maximumRange;
            Slots = slots;
            Tons = tons;
            Speed = speed;
            AmmoPerTon = ammoPerTon;
            DamagePerSecond = damagePerSecond;
            DamagePerHeat = damagePerHeat;
            DpsPerTon = DpsPerTon;
            HeatPerSecond = heatPerSecond;
            Impulse = impulse;
            Health = health;
            Costs = costs;
        }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(EnumNameConverter<HardpointType>))]
        public HardpointType Type { get; }

        [JsonPropertyName("faction")]
        public Faction Faction { get; }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("damage")]
        public double Damage { get; }

        [JsonPropertyName("heat")]
        public double Heat { get; }

        [JsonPropertyName("cooldown")]
        public double Cooldown { get; }

        [JsonPropertyName("minimumRange")]
        public int MinimumRange { get; }

        [JsonPropertyName("optimalRange")]
        public int OptimalRange { get; }

        [JsonPropertyName("maximumRange")]
        public int MaximumRange { get; }

        [JsonPropertyName("slots")]
        public int Slots { get; }

        [JsonPropertyName("tons")]
        public double Tons { get; }

        [JsonPropertyName("speed")]
        public int? Speed { get; }

        [JsonPropertyName("ammoPerTon")]
        public double? AmmoPerTon { get; }

        [JsonPropertyName("dps")]
        public double DamagePerSecond { get; }

        [JsonPropertyName("dph")]
        public double DamagePerHeat { get; }

        [JsonPropertyName("dpst")]
        public double DpsPerTon { get; }

        [JsonPropertyName("heat")]
        public double HeatPerSecond { get; }

        [JsonPropertyName("impulse")]
        public double Impulse { get; }

        [JsonPropertyName("health")]
        public double Health { get; }

        [JsonPropertyName("costs")]
        public int? Costs { get; }
    }
}
