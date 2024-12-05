namespace AdventOfCode2024.Shared;

public static class ListExtensions
{
    /// <summary>
    /// Checks if any of the provided values are contained in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool ContainsAny<T>(this List<T> list, IEnumerable<T> values)
        => values.Any(list.Contains);

    public static bool IsNullOrEmpty<T>(this List<T> list)
    {
        return list == null || list.Count == 0;
    }
}
