using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;
using MechParser.NET.Mechs;
using MechParser.NET.Mechs.Parts;
using MechParser.NET.Mechs.Slots;

namespace MechParser.NET.Smurfy
{
    [PublicAPI]
    public static class SmurfyCsvImporter
    {
        public static IEnumerable<SmurfyMech> ReadCsvPaste(string path, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.InvariantCulture;

            using var reader = new StreamReader(path);
            var configuration = new CsvConfiguration(culture) {HasHeaderRecord = false};
            using var parser = new CsvParser(reader, configuration);
            using var csvReader = new CsvReader(parser);

            var variant = string.Empty;

            while (csvReader.Read())
            {
                if (csvReader.GetField(1) == "Left Arm")
                {
                    variant = csvReader.GetField(0);
                    var tonnageOpeningParentheses = variant.LastIndexOf('(');
                    variant = variant.Substring(0, tonnageOpeningParentheses - 1).Trim();

                    continue;
                }

                var mech = csvReader.GetRecord<SmurfyMech>();
                mech.Variant = variant;

                yield return mech;
            }
        }

        private static List<ModuleSlot> ParseMissiles(string text, ref int i)
        {
            var missiles = new List<ModuleSlot>();

            text = text.Replace(" ", "");

            var openingIndex = text.IndexOf('(', i);
            var closingIndex = text.IndexOf(')', i);
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
                var missile = new ModuleSlot(ModuleType.Missile, width);
                missiles.Add(missile);
            }

            i = closingIndex;
            return missiles;
        }

        public static Part ParsePart(PartType type, string text)
        {
            if (text == string.Empty)
            {
                return new Part(type, new List<ModuleSlot>());
            }

            text = text.Replace(" ", string.Empty).ToLowerInvariant();
            var modules = new List<ModuleSlot>();

            for (var i = 0; i < text.Length; i++)
            {
                var character = text[i];

                if (char.IsDigit(character))
                {
                    var amount = (int) char.GetNumericValue(character);
                    var moduleTypeChar = text[i + 1];
                    var moduleType = moduleTypeChar switch
                    {
                        'b' => ModuleType.Ballistic,
                        'e' => ModuleType.Energy,
                        'm' => ModuleType.Missile,
                        _ when text.IndexOf("ams", i, StringComparison.InvariantCulture) != -1 => ModuleType.Ams,
                        _ when text.IndexOf("ecm", i, StringComparison.InvariantCulture) != -1 => ModuleType.Ecm,
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    int? size = null;

                    if (moduleType == ModuleType.Missile)
                    {
                        var missiles = ParseMissiles(text, ref i);
                        modules.AddRange(missiles);
                        continue;
                    }

                    for (var j = 0; j < amount; j++)
                    {
                        var module = new ModuleSlot(moduleType, size);
                        modules.Add(module);
                    }
                }
            }

            return new Part(type, modules);
        }

        public static IEnumerable<Mech> ParseCsvPaste(string path, CultureInfo? culture = null)
        {
            var smurfyMechs = ReadCsvPaste(path, culture);
            var partTypes = Enum.GetValues<PartType>();

            foreach (var smurfyMech in smurfyMechs)
            {
                var variant = smurfyMech.Variant;
                var model = smurfyMech.Model
                    .Replace("\nHero", string.Empty)
                    .Replace("\nChampion", string.Empty);

                var parts = new Dictionary<PartType, Part>();

                foreach (var type in partTypes)
                {
                    var partString = smurfyMech.GetPartString(type);
                    var part = ParsePart(type, partString);

                    parts.Add(type, part);
                }

                int.TryParse(smurfyMech.JumpJets, out var jumpJets);
                var ecm = smurfyMech.Ecm.Equals("yes", StringComparison.InvariantCultureIgnoreCase);
                var masc = smurfyMech.Masc.Equals("no", StringComparison.InvariantCultureIgnoreCase);
                var (minEngine, maxEngine, defaultEngine) = smurfyMech.ParseEngine();

                var hardpoints = new Dictionary<ModuleType, int>();
                var hardpointsString = smurfyMech.Hardpoints.Split('\n');
                hardpoints[ModuleType.Energy] = int.Parse(hardpointsString[0]);
                hardpoints[ModuleType.Ballistic] = int.Parse(hardpointsString[1]);
                hardpoints[ModuleType.Missile] = int.Parse(hardpointsString[2]);

                yield return new Mech(
                    variant,
                    model,
                    parts,
                    jumpJets,
                    ecm,
                    masc,
                    minEngine,
                    maxEngine,
                    defaultEngine,
                    hardpoints);
            }
        }
    }
}
