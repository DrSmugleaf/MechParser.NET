namespace MechParser.NET.Extensions
{
    public static class IntExtensions
    {
        public static int? ParseOrNull(string text)
        {
            return int.TryParse(text, out var i) ? i : null;
        }
    }
}
