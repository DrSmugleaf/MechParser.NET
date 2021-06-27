using System;
using System.Drawing;

namespace MechParser.NET.Mechs.Slots
{
    public static class HardpointTypeExtensions
    {
        public static string Name(this HardpointType type)
        {
            return type switch
            {
                HardpointType.Ballistic => "Ballistic",
                HardpointType.Energy => "Energy",
                HardpointType.Missile => "Missile",
                HardpointType.Ams => "AMS",
                HardpointType.Ecm => "ECM",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static string Abbreviation(this HardpointType type)
        {
            return type switch
            {
                HardpointType.Ballistic => "B",
                HardpointType.Energy => "E",
                HardpointType.Missile => "M",
                HardpointType.Ams => "AMS",
                HardpointType.Ecm => "ECM",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static Color Hue(this HardpointType type)
        {
            return type switch
            {
                HardpointType.Ballistic => Color.FromArgb(132, 107, 149),
                HardpointType.Energy => Color.FromArgb(203, 120, 52),
                HardpointType.Missile => Color.FromArgb(106, 205, 151),
                HardpointType.Ams => Color.FromArgb(204, 102, 100),
                HardpointType.Ecm => Color.FromArgb(161, 176, 81),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
