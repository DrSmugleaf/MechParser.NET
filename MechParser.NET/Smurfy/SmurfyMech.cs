using System;
using CsvHelper.Configuration.Attributes;
using MechParser.NET.Mechs.Parts;

namespace MechParser.NET.Smurfy
{
    public record SmurfyMech
    {
        public string Variant { get; internal set; } = string.Empty;

        [Index(0)]
        public string Model { get; set; } = string.Empty;

        [Index(1)]
        public string LeftArm { get; set; } = string.Empty;

        [Index(2)]
        public string LeftTorso { get; set; } = string.Empty;

        [Index(3)]
        public string Center { get; set; } = string.Empty;

        [Index(4)]
        public string RightTorso { get; set; } = string.Empty;

        [Index(5)]
        public string RightArm { get; set; } = string.Empty;

        [Index(6)]
        public string Head { get; set; } = string.Empty;

        [Index(7)]
        public string JJ { get; set; } = string.Empty;

        [Index(8)]
        public string ECM { get; set; } = string.Empty;

        [Index(9)]
        public string MASC { get; set; } = string.Empty;

        [Index(10)]
        public string Engines { get; set; } = string.Empty;

        [Index(11)]
        public string Hardpoints { get; set; } = string.Empty;

        [Index(12)]
        public string TorsoArm { get; set; } = string.Empty;

        [Index(13)]
        public string Costs { get; set; } = string.Empty;

        public string GetPartString(PartType type)
        {
            return type switch
            {
                PartType.Head => Head,
                PartType.LeftArm => LeftArm,
                PartType.LeftTorso => LeftTorso,
                PartType.Center => Center,
                PartType.RightTorso => RightTorso,
                PartType.RightArm => RightArm,
                PartType.LeftLeg => string.Empty,
                PartType.RightLeg => string.Empty,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
