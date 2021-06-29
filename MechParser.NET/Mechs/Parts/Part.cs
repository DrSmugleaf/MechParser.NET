using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MechParser.NET.Json;
using MechParser.NET.Mechs.Slots;

namespace MechParser.NET.Mechs.Parts
{
    [PublicAPI]
    public record Part
    {
        public Part(PartType type, List<Hardpoint> hardpoints)
        {
            Name = type.Name();
            Type = type;
            Hardpoints = hardpoints;
            Slots = type.Slots();
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(EnumNameConverter<PartType>))]
        public PartType Type { get; }

        [JsonPropertyName("hardpoints")]
        public List<Hardpoint> Hardpoints { get; }

        [JsonPropertyName("slots")]
        public int Slots { get; }

        public override string ToString()
        {
            var str = new StringBuilder(Name);

            if (Hardpoints.Count == 0)
            {
                return str.ToString();
            }

            str.Append(": ");
            var slotAmount = new Dictionary<HardpointType, int>();

            foreach (var slot in Hardpoints)
            {
                if (!slotAmount.ContainsKey(slot.Type))
                {
                    slotAmount[slot.Type] = 1;
                    continue;
                }

                slotAmount[slot.Type]++;
            }

            foreach (var (type, amount) in slotAmount)
            {
                str.Append($"{amount} {type.Abbreviation()} ");
            }

            return str.ToString().Trim();
        }
    }
}
