using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;
using MechParser.NET.Mechs;
using MechParser.NET.Mechs.Parts;
using MechParser.NET.Mechs.Slots;

namespace MechParser.NET.Smurfy
{
    [PublicAPI]
    public static class SmurfyImporter
    {
        public static IEnumerable<SmurfyMech> ReadCsvPaste(string path, CultureInfo? culture = null)
        {
            culture ??= CultureInfo.InvariantCulture;

            using var reader = new StreamReader(path);
            var configuration = new CsvConfiguration(culture);
            using var parser = new CsvParser(reader, configuration);
            using var csvReader = new CsvReader(parser);

            var mechName = string.Empty;

            while (csvReader.Read())
            {
                if (csvReader.GetField(1) == "Left Arm")
                {
                    mechName = csvReader.GetField(0);
                    var tonnageOpeningParentheses = mechName.LastIndexOf('(');
                    mechName = mechName.Substring(0, tonnageOpeningParentheses - 1).Trim();

                    continue;
                }

                var mech = csvReader.GetRecord<SmurfyMech>();
                mech.Name = mechName;

                yield return mech;
            }
        }

        public static List<ModuleSlot> ParseMissile(string text, ref int i)
        {
            var missiles = new List<ModuleSlot>();

            var openingIndex = text.IndexOf('(', i);
            var closingIndex = text.IndexOf(')', i);
            var sizeString = text.Substring(openingIndex + 1, closingIndex - 1 - openingIndex);

            for (var j = 0; j < sizeString.Length; j++)
            {
                var missileSizeString = new StringBuilder();
                var currentSizeChar = sizeString[j];

                if (currentSizeChar == ')')
                {
                    break;
                }

                while (sizeString.Length > j && char.IsDigit(sizeString[j]))
                {
                    missileSizeString.Append(currentSizeChar);
                    j++;
                }

                var missileSize = int.Parse(missileSizeString.ToString());

                switch (currentSizeChar)
                {
                    case 'x':
                        var amountChars = sizeString
                            .Substring(j)
                            .TakeWhile(char.IsDigit)
                            .ToArray();
                        var amountString = new string(amountChars);
                        var amount = int.Parse(amountString);

                        for (var l = 0; l < amount; l++)
                        {
                            var missile = new ModuleSlot(ModuleType.Missile, missileSize);
                            missiles.Add(missile);
                        }

                        j += amountChars.Length;
                        break;
                }
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
                    var moduleTypeChar = text[++i];
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
                        var missiles = ParseMissile(text, ref i);
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

                yield return new Mech(smurfyMech.Name, model, parts);
            }
        }
    }
}
