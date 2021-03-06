using System;

namespace MechParser.NET.Mechs.Parts
{
    public static class PartExtensions
    {
        public static string Name(this PartType type)
        {
            return type switch
            {
                PartType.Head => "Head",
                PartType.LeftArm => "Left Arm",
                PartType.LeftTorso => "Left Torso",
                PartType.Center => "Center",
                PartType.RightTorso => "Right Torso",
                PartType.RightArm => "Right Arm",
                PartType.LeftLeg => "Left Leg",
                PartType.RightLeg => "Right Leg",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static string Abbreviation(this PartType type)
        {
            return type switch
            {
                PartType.Head => "Head",
                PartType.LeftArm => "LA",
                PartType.LeftTorso => "LT",
                PartType.Center => "CT",
                PartType.RightTorso => "RT",
                PartType.RightArm => "RA",
                PartType.LeftLeg => "LL",
                PartType.RightLeg => "RL",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static int Slots(this PartType type)
        {
            return (int) (type switch
            {
                PartType.Head => PartSlots.Head,
                PartType.LeftArm => PartSlots.Arm,
                PartType.LeftTorso => PartSlots.Side,
                PartType.Center => PartSlots.Center,
                PartType.RightTorso => PartSlots.Side,
                PartType.RightArm => PartSlots.Arm,
                PartType.LeftLeg => PartSlots.Leg,
                PartType.RightLeg => PartSlots.Leg,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            });
        }
    }
}
