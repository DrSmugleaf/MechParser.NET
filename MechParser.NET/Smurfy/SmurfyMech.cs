using System;
using CsvHelper.Configuration.Attributes;
using MechParser.NET.Mechs.Parts;

namespace MechParser.NET.Smurfy
{
    public record SmurfyMech
    {
        public string Name { get; internal set; }

        [Index(0)]
        public string Model { get; set; }

        [Index(1)]
        public string LeftArm { get; set; }

        [Index(2)]
        public string LeftTorso { get; set; }

        [Index(3)]
        public string Center { get; set; }

        [Index(4)]
        public string RightTorso { get; set; }

        [Index(5)]
        public string RightArm { get; set; }

        [Index(6)]
        public string Head { get; set; }

        [Index(7)]
        public string JJ { get; set; }

        [Index(8)]
        public string ECM { get; set; }

        [Index(9)]
        public string MASC { get; set; }

        [Index(10)]
        public string Engines { get; set; }

        [Index(11)]
        public string Hardpoints { get; set; }

        [Index(12)]
        public string TorsoArm { get; set; }

        [Index(13)]
        public string Costs { get; set; }

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
