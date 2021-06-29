using System.Drawing;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MechParser.NET.Json;

namespace MechParser.NET.Mechs.Slots
{
    [PublicAPI]
    public record Hardpoint
    {
        public Hardpoint(HardpointType type, int? size)
        {
            Name = type.Name();
            Abbreviation = type.Abbreviation();
            Type = type;
            Color = type.Hue();
            Size = size;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(EnumNameConverter<HardpointType>))]
        public HardpointType Type { get; set; }

        [JsonPropertyName("color")]
        [JsonConverter(typeof(ColorHexConverter))]
        public Color Color { get; set; }

        [JsonPropertyName("size")]
        public int? Size { get; set; }

        public override string ToString()
        {
            return $"{Name}{(Size != null ? $" {Size}" : string.Empty)}";
        }
    }
}
