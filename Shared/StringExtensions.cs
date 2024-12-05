namespace AdventOfCode2024.Shared;

public static class StringExtensions
{
    /// <summary>
    /// Minor simplification to check if a string is null or empty.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string str)
        => string.IsNullOrEmpty(str);
}
