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

    /// <summary>
    /// Checks if the list is either null or empty.
    /// Returns false only if the list is not empty and there is at least one value in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this List<T>? list)
    {
        return list == null || list.Count == 0;
    }
}
