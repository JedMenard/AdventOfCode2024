
namespace AdventOfCode2024.Shared;

public static class DictionaryExtensions
{
    public static void AddToListOrInsert<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue val)
    {
        if (!dictionary.ContainsKey(key)) {
            dictionary[key] = new List<TValue>();
        }

        dictionary[key].Add(val);
    }
}
