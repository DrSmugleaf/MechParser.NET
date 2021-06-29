using System.Collections.Generic;
using System.Collections.Immutable;

namespace MechParser.NET.Extensions
{
    public static class DictionaryExtensions
    {
        public static ImmutableDictionary<TKey, TValue> ToImmutable<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary)
            where TKey : notnull
        {
            return dictionary.ToImmutableDictionary(kvPair => kvPair.Key, kvPair => kvPair.Value);
        }
    }
}
