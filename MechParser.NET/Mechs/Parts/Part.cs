using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MechParser.NET.Mechs.Slots;

namespace MechParser.NET.Mechs.Parts
{
    [PublicAPI]
    public record Part
    {
        public Part(PartType type, List<ModuleSlot> slots)
        {
            Name = type.Name();
            Type = type;
            Slots = slots;
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("type")]
        public PartType Type { get; }

        [JsonPropertyName("slots")]
        public List<ModuleSlot> Slots { get; }

        public override string ToString()
        {
            var str = new StringBuilder(Name);

            if (Slots.Count == 0)
            {
                return str.ToString();
            }

            str.Append(": ");
            var slotAmount = new Dictionary<ModuleType, int>();

            foreach (var slot in Slots)
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
