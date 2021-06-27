using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MechParser.NET.Mechs.Engines;
using MechParser.NET.Mechs.Parts;
using MechParser.NET.Mechs.Slots;

namespace MechParser.NET.Mechs
{
    [PublicAPI]
    public record Mech
    {
        public Mech(
            string variant,
            string model,
            Dictionary<PartType, Part> parts,
            int jumpJets,
            bool ecm,
            bool masc,
            int minimumEngine,
            int maximumEngine,
            Engine defaultEngine,
            Dictionary<ModuleType, int> hardPoints)
        {
            Variant = variant;
            Model = model;
            Parts = parts;
            JumpJets = jumpJets;
            Ecm = ecm;
            Masc = masc;
            MinimumEngine = minimumEngine;
            MaximumEngine = maximumEngine;
            DefaultEngine = defaultEngine;
            HardPoints = hardPoints;
        }

        [JsonPropertyName("variant")]
        public string Variant { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("parts")]
        public Dictionary<PartType, Part> Parts { get; set; }

        [JsonPropertyName("jumpjets")]
        public int JumpJets { get; set; }

        [JsonPropertyName("ecm")]
        public bool Ecm { get; set; }

        [JsonPropertyName("masc")]
        public bool Masc { get; set; }

        [JsonPropertyName("minimumEngine")]
        public int MinimumEngine { get; set; }

        [JsonPropertyName("maximumEngine")]
        public int MaximumEngine { get; set; }

        [JsonPropertyName("defaultEngine")]
        public Engine DefaultEngine { get; set; }

        [JsonPropertyName("hardpoints")]
        public Dictionary<ModuleType, int> HardPoints { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder(Model);

            if (Parts.Count == 0)
            {
                return str.ToString();
            }

            str.Append(":\n");

            foreach (var part in Parts.Values)
            {
                if (part.Slots.Count == 0)
                {
                    continue;
                }

                str.Append($"{part}\n");
            }

            return str.ToString().Trim();
        }
    }
}
