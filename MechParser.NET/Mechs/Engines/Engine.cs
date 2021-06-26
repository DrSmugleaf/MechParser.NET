using System.Text.Json.Serialization;

namespace MechParser.NET.Mechs.Engines
{
    public record Engine
    {
        public Engine(EngineType type, int level)
        {
            Type = type;
            Level = level;
        }

        [JsonPropertyName("type")]
        public EngineType Type { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }
    }
}
