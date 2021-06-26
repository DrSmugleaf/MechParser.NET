using System;
using System.Drawing;

namespace MechParser.NET.Mechs.Slots
{
    public static class ModuleTypeExtensions
    {
        public static string Name(this ModuleType type)
        {
            return type switch
            {
                ModuleType.Ballistic => "Ballistic",
                ModuleType.Energy => "Energy",
                ModuleType.Missile => "Missile",
                ModuleType.Ams => "AMS",
                ModuleType.Ecm => "ECM",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static string Abbreviation(this ModuleType type)
        {
            return type switch
            {
                ModuleType.Ballistic => "B",
                ModuleType.Energy => "E",
                ModuleType.Missile => "M",
                ModuleType.Ams => "AMS",
                ModuleType.Ecm => "ECM",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static Color Hue(this ModuleType type)
        {
            return type switch
            {
                ModuleType.Ballistic => Color.FromArgb(132, 107, 149),
                ModuleType.Energy => Color.FromArgb(203, 120, 52),
                ModuleType.Missile => Color.FromArgb(106, 205, 151),
                ModuleType.Ams => Color.FromArgb(204, 102, 100),
                ModuleType.Ecm => Color.FromArgb(161, 176, 81),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
