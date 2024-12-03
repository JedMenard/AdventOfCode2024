namespace AdventOfCode2024.Days.Day02;

public class Report
{
    public List<int> Levels;

    public Report(string input)
    {
        this.Levels = input.Split(" ")
            .Select(val => int.Parse(val))
            .ToList();
    }

    /// <summary>
    /// Checks if the original list is safe, or, if not,
    /// if removing one element from the list would make it tolerable.
    /// </summary>
    /// <returns></returns>
    public bool IsSafe()
    {
        // Find the index of the first unsafe value in the list.
        int? indexOfUnsafeLevel = this.GetIndexOfUnsafeLevel(this.Levels);

        // If no unsafe values were found, the list is already safe.
        if (!indexOfUnsafeLevel.HasValue)
        {
            return true;
        }

        // An unsafe value was found.
        // The only way the list can be tolerable now is if removing the first unsafe value,
        // the value before that, or the value after it would make it safe.
        // Try all three, accounting for safe bounds.
        for (int i = Math.Min(indexOfUnsafeLevel.Value, 0); i < Math.Max(indexOfUnsafeLevel.Value + 1, this.Levels.Count); i++)
        {
            // First, make a copy of the input list.
            List<int> levelsCopy = new List<int>(this.Levels);

            // Now remove the appropriate value.
            levelsCopy.RemoveAt(i);

            // Test the validity again.
            if (!this.GetIndexOfUnsafeLevel(levelsCopy).HasValue)
            {
                return true;
            }
        }

        // The list is intolerable.
        return false;
    }

    /// <summary>
    /// Checks if a list is considered safe, and returns the index of the first unsafe level, if any.
    /// If the list is safe, returns null.
    /// </summary>
    /// <param name="levels"></param>
    /// <returns></returns>
    private int? GetIndexOfUnsafeLevel(List<int> levels)
    {
        // Determine if the first two numbers in the list are increasing, decreasing, or the same.
        int sign = this.GetSign(levels[0], levels[1]);

        // Check the validity of the list, starting again with the first two values.
        for (int i = 0; i < levels.Count - 1; i++)
        {
            // Determine whether the current value and next value are increasing, decreasing, or the same.
            int currentSign = this.GetSign(levels[i], levels[i + 1]);

            // All lists must be uniformly increasing or decreasing,
            // so return false if the current sign differs from the original sign.
            if (currentSign != sign)
            {
                return i;
            }

            // All lists must change value by 1-3, so make sure the difference is within the expected range.
            int difference = Math.Abs(levels[i] - levels[i + 1]);
            if (difference < 1 || difference > 3)
            {
                return i;
            }
        }

        // No conditions failed, return true.
        return null;
    }

    /// <summary>
    /// Returns a signed integer representing the direction of difference between two numbers.
    /// If the numbers are the same, returns zero.
    /// If the first value is larger (e.g. the values are decreasing), returns -1.
    /// If the second value is larger (e.g. the values are increasing), returns 1.
    /// </summary>
    /// <param name="firstVal"></param>
    /// <param name="secondVal"></param>
    /// <returns></returns>
    private int GetSign(int firstVal, int secondVal)
    {
        if (firstVal == secondVal)
        {
            return 0;
        }

        return secondVal - firstVal > 0 ? 1 : -1;
    }
}
