
namespace AdventOfCode2024.Shared;

public static class DictionaryExtensions
{
    /// <summary>
    /// 
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
}
