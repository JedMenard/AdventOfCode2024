namespace AdventOfCode2024.Shared;

public static class EnumerableExtensions
{
    /// <summary>
    /// Joins the values in an enumerable into a string using the provided delimiter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static string Join<T>(this IEnumerable<T> enumerable, string delimiter)
    {
        string str = "";
        bool isFirst = true;
        foreach (T val in enumerable)
        {
            if (!isFirst)
            {
                str += delimiter;
            }

            str += val.ToString();
            isFirst = false;
        }

        return str;
    }

    /// <summary>
    /// Checks if the enumerable is either null or empty.
    /// Returns false only if the enumerable is not empty and has at least one value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
    {
        return enumerable == null || !enumerable.Any();
    }
}
