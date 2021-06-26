using System.Diagnostics.CodeAnalysis;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace MechParser.NET.Smurfy
{
    public class SmurfyMechConverter : ITypeConverter
    {
        private static readonly TypeConverterCache DefaultConverters = new();

        public string CurrentName { get; set; } = string.Empty;

        private bool IsNameHeader(IReaderRow row, [NotNullWhen(true)] out string? name)
        {
            if (row[1] == "Left Arm")
            {
                name = row[0];
                var tonnageOpeningParentheses = name.LastIndexOf('(');
                name = name.Substring(0, tonnageOpeningParentheses - 1).Trim();

                return true;
            }

            name = null;
            return false;
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (IsNameHeader(row, out var name))
            {
                CurrentName = name;
                return null!;
            }

            var mech = (SmurfyMech) DefaultConverters.GetConverter<SmurfyMech>().ConvertFromString(text, row, memberMapData);
            mech.Name = CurrentName;

            return mech;
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return DefaultConverters.GetConverter<SmurfyMech>().ConvertToString(value, row, memberMapData);
        }
    }
}
