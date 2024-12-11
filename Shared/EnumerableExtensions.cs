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
}
