namespace AdventOfCode2024.Shared;

public static class CollectionExtensions
{
    /// <summary>
    /// Checks if any of the provided values are contained in the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool ContainsAny<T>(this ICollection<T> collection, IEnumerable<T> values)
        => values.Any(collection.Contains);

    /// <summary>
    /// Checks if the collection is either null or empty.
    /// Returns false only if the collection is not empty has at least one value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this ICollection<T>? collection)
    {
        return collection == null || collection.Count == 0;
    }
}
