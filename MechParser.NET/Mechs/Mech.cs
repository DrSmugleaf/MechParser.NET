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
            Faction faction,
            string variant,
            string model,
            int tonnage,
            Dictionary<PartType, Part> parts,
            int jumpJets,
            bool ecm,
            bool masc,
            int minimumEngine,
            int maximumEngine,
            Engine defaultEngine,
            Dictionary<ModuleType, int> hardPoints,
            double torsoYaw,
            double torsoPitch,
            double armYaw,
            double armPitch,
            int? mcCost,
            int? cBillsCost)
        {
            Faction = faction;
            Variant = variant;
            Model = model;
            Tonnage = tonnage;
            Parts = parts;
            JumpJets = jumpJets;
            Ecm = ecm;
            Masc = masc;
            MinimumEngine = minimumEngine;
            MaximumEngine = maximumEngine;
            DefaultEngine = defaultEngine;
            HardPoints = hardPoints;
            TorsoYaw = torsoYaw;
            TorsoPitch = torsoPitch;
            ArmYaw = armYaw;
            ArmPitch = armPitch;
            McCost = mcCost;
            CBillsCost = cBillsCost;
        }

        [JsonPropertyName("faction")]
        public Faction Faction { get; }

        [JsonPropertyName("variant")]
        public string Variant { get; }

        [JsonPropertyName("model")]
        public string Model { get; }

        [JsonPropertyName("tonnage")]
        public int Tonnage { get; }

        [JsonPropertyName("parts")]
        public Dictionary<PartType, Part> Parts { get; }

        [JsonPropertyName("jumpjets")]
        public int JumpJets { get; }

        [JsonPropertyName("ecm")]
        public bool Ecm { get; }

        [JsonPropertyName("masc")]
        public bool Masc { get; }

        [JsonPropertyName("minimumEngine")]
        public int MinimumEngine { get; }

        [JsonPropertyName("maximumEngine")]
        public int MaximumEngine { get; }

        [JsonPropertyName("defaultEngine")]
        public Engine DefaultEngine { get; }

        [JsonPropertyName("hardpoints")]
        public Dictionary<ModuleType, int> HardPoints { get; }

        [JsonPropertyName("torsoYaw")]
        public double TorsoYaw { get; }

        [JsonPropertyName("torsoPitch")]
        public double TorsoPitch { get; }

        [JsonPropertyName("armYaw")]
        public double ArmYaw { get; }

        [JsonPropertyName("armPitch")]
        public double ArmPitch { get; }

        [JsonPropertyName("mcCost")]
        public int? McCost { get; }

        [JsonPropertyName("cBillsCost")]
        public int? CBillsCost { get; }

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
                if (part.Hardpoints.Count == 0)
                {
                    continue;
                }

                str.Append($"{part}\n");
            }

            return str.ToString().Trim();
        }
    }
}
