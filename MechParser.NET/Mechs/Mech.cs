using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MechParser.NET.Mechs.Parts;

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
            bool masc)
        {
            Variant = variant;
            Model = model;
            Parts = parts;
            JumpJets = jumpJets;
            Ecm = ecm;
            Masc = masc;
        }

        [JsonPropertyName("variant")]
        public string Variant { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("parts")]
        public Dictionary<PartType, Part> Parts { get; set; }

        [JsonPropertyName("jj")]
        public int JumpJets { get; set; }

        [JsonPropertyName("ecm")]
        public bool Ecm { get; set; }

        [JsonPropertyName("masc")]
        public bool Masc { get; set; }

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
