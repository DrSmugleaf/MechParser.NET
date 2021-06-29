using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MechParser.NET.Json;
using MechParser.NET.Mechs.Engines;
using MechParser.NET.Mechs.Parts;
using MechParser.NET.Mechs.Slots;

namespace MechParser.NET.Mechs
{
    [PublicAPI]
    public record Mech : IComparable<Mech>
    {
        public Mech(
            Faction faction,
            string model,
            string variant,
            int tonnage,
            Dictionary<PartType, Part> parts,
            int jumpJets,
            bool ecm,
            bool masc,
            int minimumEngine,
            int maximumEngine,
            Engine defaultEngine,
            Dictionary<HardpointType, int> hardPoints,
            double torsoYaw,
            double torsoPitch,
            double armYaw,
            double armPitch,
            int? mcCost,
            int? cBillsCost,
            bool champion,
            bool hero)
        {
            Faction = faction;
            Model = model;
            Variant = variant;
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
            Champion = champion;
            Hero = hero;
        }

        [JsonPropertyName("faction")]
        [JsonConverter(typeof(EnumNameConverter<Faction>))]
        public Faction Faction { get; }

        [JsonPropertyName("model")]
        public string Model { get; }

        [JsonPropertyName("variant")]
        public string Variant { get; }

        [JsonPropertyName("tonnage")]
        public int Tonnage { get; }

        [JsonPropertyName("tonnageClass")]
        [JsonConverter(typeof(EnumNameConverter<TonnageClass>))]
        public TonnageClass TonnageClass
        {
            get
            {
                return Tonnage switch
                {
                    < 40 => TonnageClass.Light,
                    < 60 => TonnageClass.Medium,
                    < 80 => TonnageClass.Heavy,
                    _ => TonnageClass.Assault
                };
            }
        }

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
        public Dictionary<HardpointType, int> HardPoints { get; }

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

        [JsonPropertyName("champion")]
        public bool Champion { get; }

        [JsonPropertyName("hero")]
        public bool Hero { get; }

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

        public int CompareTo(Mech? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            var factionComparison = Faction.CompareTo(other.Faction);
            if (factionComparison != 0) return factionComparison;
            var modelComparison = string.Compare(Model, other.Model, StringComparison.Ordinal);
            if (modelComparison != 0) return modelComparison;
            var variantComparison = string.Compare(Variant, other.Variant, StringComparison.Ordinal);
            if (variantComparison != 0) return variantComparison;
            return Tonnage.CompareTo(other.Tonnage);
        }
    }
}
