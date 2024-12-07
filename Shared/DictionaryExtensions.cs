
namespace AdventOfCode2024.Shared;

public static class DictionaryExtensions
{
    /// <summary>
    /// Helper to add an element to a list in a dictionary at the given key.
    /// If the key is not in the dictionary yet, instantiates an empty list and inserts it.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="val"></param>
    public static void AddToListOrInsert<TKey, TValue> (this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue val) where TKey : notnull
    {
        if (!dictionary.ContainsKey(key)) {
            dictionary[key] = new List<TValue>();
        }

        dictionary[key].Add(val);
    }

    /// <summary>
    /// Helper to add an element to a set in a dictionary at the given key.
    /// If the key is not in the dictionary yet, instantiates an empty set and inserts it.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="val"></param>
    public static void AddToSetOrInsert<TKey, TValue> (this Dictionary<TKey, HashSet<TValue>> dictionary, TKey key, TValue val) where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary[key] = new HashSet<TValue>();
        }

        dictionary[key].Add(val);
    }
}
