using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using MechParser.NET.Extensions;
using MechParser.NET.Mechs;
using MechParser.NET.Mechs.Engines;
using MechParser.NET.Mechs.Parts;
using MechParser.NET.Mechs.Slots;

namespace MechParser.NET.Smurfy
{
    public class SmurfyMechRow
    {
        private const string BallisticSpanSelector = "span.label-mech-hardpoint-ballistic";
        private const string EnergySpanSelector = "span.label-mech-hardpoint-beam";
        private const string MissileSpanSelector = "span.label-mech-hardpoint-missle";
        private const string AmsSpanSelector = "span.label-mech-hardpoint-ams";
        private const string EcmSpanSelector = "span.label-mech-hardpoint-ecm";

        private const string BallisticHardpointSelector = "div.label-mech-hardpoint-ballistic";
        private const string EnergyHardpointSelector = "div.label-mech-hardpoint-beam";
        private const string MissileHardpointSelector = "div.label-mech-hardpoint-missle";

        public SmurfyMechRow(IHtmlTableRowElement row)
        {
            Variant = row.Cells[0];
            LeftArm = row.Cells[1];
            LeftTorso = row.Cells[2];
            Center = row.Cells[3];
            RightTorso = row.Cells[4];
            RightArm = row.Cells[5];
            Head = row.Cells[6];
            JumpJets = row.Cells[7];
            Ecm = row.Cells[8];
            Masc = row.Cells[9];
            Engines = row.Cells[10];
            Hardpoints = row.Cells[11];
            TorsoArm = row.Cells[12];
            Cost = row.Cells[13];
        }

        private IHtmlTableCellElement Variant { get; }

        private IHtmlTableCellElement LeftArm { get; }

        private IHtmlTableCellElement LeftTorso { get; }

        private IHtmlTableCellElement Center { get; }

        private IHtmlTableCellElement RightTorso { get; }

        private IHtmlTableCellElement RightArm { get; }

        private IHtmlTableCellElement Head { get; }

        private IHtmlTableCellElement JumpJets { get; }

        private IHtmlTableCellElement Ecm { get; }

        private IHtmlTableCellElement Masc { get; }

        private IHtmlTableCellElement Engines { get; }

        private IHtmlTableCellElement Hardpoints { get; }

        private IHtmlTableCellElement TorsoArm { get; }

        private IHtmlTableCellElement Cost { get; }

        public Faction ParseFaction()
        {
            return Variant.ParentElement!.GetAttribute("data-mechfilter-faction")!.ToLowerInvariant() switch
            {
                "innersphere" => Faction.InnerSphere,
                "clan" => Faction.Clan,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public string ParseVariant()
        {
            return Variant.QuerySelector("a")!.TextContent;
        }

        private int ParseHardpointCount(IElement element)
        {
            return int.Parse(element.QuerySelector("span.count")!.TextContent);
        }

        private IEnumerable<Hardpoint> ParseMissiles(string text)
        {
            text = text.Replace(" ", "");

            var openingIndex = text.IndexOf('(');
            var closingIndex = text.IndexOf(')');
            var sizeString = text.Substring(openingIndex + 1, closingIndex - 1 - openingIndex);
            var sizes = new List<int>();

            for (var j = 0; j < sizeString.Length; j++)
            {
                var missileSizeString = sizeString.TakeWhile(char.IsDigit).ToArray();
                var size = int.Parse(missileSizeString);

                j += missileSizeString.Length;

                if (sizeString.Length <= j)
                {
                    sizes.Add(size);
                    break;
                }

                switch (sizeString[j])
                {
                    case 'x':
                        var setsChars = sizeString
                            .Substring(j + 1)
                            .TakeWhile(char.IsDigit)
                            .ToArray();
                        var sets = int.Parse(setsChars);

                        for (var k = 0; k < sets; k++)
                        {
                            sizes.Add(size);
                        }

                        j += setsChars.Length;
                        break;
                    default:
                        sizes.Add(size);
                        break;
                }
            }

            foreach (var width in sizes)
            {
                yield return new Hardpoint(HardpointType.Missile, width);
            }
        }

        private List<Hardpoint> ParseHardpoints(IHtmlTableCellElement cell)
        {
            var hardpoints = new List<Hardpoint>();

            if (cell.QuerySelector(BallisticSpanSelector) is { } ballisticSpan)
            {
                var count = ParseHardpointCount(ballisticSpan);

                for (var i = 0; i < count; i++)
                {
                    var hardpoint = new Hardpoint(HardpointType.Ballistic, null);
                    hardpoints.Add(hardpoint);
                }
            }

            if (cell.QuerySelector(EnergySpanSelector) is { } energySpan)
            {
                var count = ParseHardpointCount(energySpan);

                for (var i = 0; i < count; i++)
                {
                    var hardpoint = new Hardpoint(HardpointType.Energy, null);
                    hardpoints.Add(hardpoint);
                }
            }

            if (cell.QuerySelector(MissileSpanSelector)?.TextContent.Trim() is { } missileSpan)
            {
                foreach (var missile in ParseMissiles(missileSpan))
                {
                    hardpoints.Add(missile);
                }
            }

            if (cell.QuerySelector(AmsSpanSelector) is { } amsSpan)
            {
                var count = ParseHardpointCount(amsSpan);

                for (var i = 0; i < count; i++)
                {
                    var hardpoint = new Hardpoint(HardpointType.Ams, null);
                    hardpoints.Add(hardpoint);
                }
            }

            if (cell.QuerySelector(EcmSpanSelector) is { } ecmSpan)
            {
                var count = ParseHardpointCount(ecmSpan);

                for (var i = 0; i < count; i++)
                {
                    var hardpoint = new Hardpoint(HardpointType.Ecm, null);
                    hardpoints.Add(hardpoint);
                }
            }

            return hardpoints;
        }

        private IHtmlTableCellElement? GetPartCell(PartType type)
        {
            return type switch
            {
                PartType.Head => Head,
                PartType.LeftArm => LeftArm,
                PartType.LeftTorso => LeftTorso,
                PartType.Center => Center,
                PartType.RightTorso => RightTorso,
                PartType.RightArm => RightArm,
                PartType.LeftLeg => null,
                PartType.RightLeg => null,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public Dictionary<PartType, Part> ParseParts()
        {
            var parts = new Dictionary<PartType, Part>();

            foreach (var type in Enum.GetValues<PartType>())
            {
                var cell = GetPartCell(type);

                if (cell == null)
                {
                    parts.Add(type, new Part(type, new List<Hardpoint>()));
                    continue;
                }

                var hardpoints = ParseHardpoints(cell);
                parts.Add(type, new Part(type, hardpoints));
            }

            return parts;
        }

        public int ParseJumpJets()
        {
            var span = JumpJets.QuerySelector("span");

            if (span == null ||
                !int.TryParse(span.TextContent, out var jumpJets))
            {
                return 0;
            }

            return jumpJets;
        }

        public bool ParseEcm()
        {
            return Ecm.QuerySelector("i.icon-ok") != null;
        }

        public bool ParseMasc()
        {
            return Masc.QuerySelector("i.icon-ok") != null;
        }

        public (int minimum, int maximum) ParseEngineRange()
        {
            var engine = Engines.QuerySelector("a.engine-popover-show")!;
            var minimum = int.Parse(engine.GetAttribute("data-engine-min")!);
            var maximum = int.Parse(engine.GetAttribute("data-engine-max")!);

            return (minimum, maximum);
        }

        public Engine ParseDefaultEngine()
        {
            var engine = Engines.QuerySelector("small") is { } engineElement
                ? engineElement.TextContent
                : Engines.TextContent.Trim();
            var engineStrings = engine.Split(" ");
            var engineType = engineStrings[0].ToLowerInvariant() switch
            {
                "std" => EngineType.STD,
                "xl" => EngineType.XL,
                "light" => EngineType.Light,
                _ => throw new ArgumentOutOfRangeException()
            };
            var engineLevel = int.Parse(engineStrings[1]);

            return new Engine(engineType, engineLevel);
        }

        public Dictionary<HardpointType, int> ParseHardpoints()
        {
            var hardpoints = new Dictionary<HardpointType, int>();

            var ballistic = int.Parse(Hardpoints.QuerySelector(BallisticHardpointSelector)!.TextContent);
            hardpoints.Add(HardpointType.Ballistic, ballistic);

            var energy = int.Parse(Hardpoints.QuerySelector(EnergyHardpointSelector)!.TextContent);
            hardpoints.Add(HardpointType.Energy, energy);

            var missile = int.Parse(Hardpoints.QuerySelector(MissileHardpointSelector)!.TextContent);
            hardpoints.Add(HardpointType.Missile, missile);

            return hardpoints;
        }

        public (double torsoYaw, double torsoPitch, double armYaw, double armPitch) ParseTwist()
        {
            var lines = TorsoArm.TextContent.Trim().Replace("°", " ").Split("\n");

            var firstLine = lines[0].Split('/');
            var secondLine = lines[1].Split('/');

            var torsoYaw = double.Parse(firstLine[0]);
            var torsoPitch = double.Parse(secondLine[0]);

            var armYaw = double.Parse(firstLine[1]);
            var armPitch = double.Parse(secondLine[1]);

            return (torsoYaw, torsoPitch, armYaw, armPitch);
        }

        public (int? mcCost, int? cBillsCost) ParseCost()
        {
            var mcCostString = Cost.QuerySelector("span.prices-mc")?.TextContent;
            var cBillsCostString = Cost.QuerySelector("span.prices-cb")?.TextContent.Replace(",", "");

            var mcCost = IntExtensions.ParseOrNull(mcCostString);
            var cBills = IntExtensions.ParseOrNull(cBillsCostString);

            return (mcCost, cBills);
        }

        public bool ParseChampion()
        {
            return Variant.QuerySelector("small")?.TextContent.ToLowerInvariant() == "champion";
        }

        public bool ParseHero()
        {
            return Variant.QuerySelector("small")?.TextContent.ToLowerInvariant() == "hero";
        }
    }
}
