namespace AdventOfCode2024.Days.DayTwo;

public class Report
{
    public List<int> Levels;

    public Report(string input)
    {
        this.Levels = input.Split(" ")
            .Select(val => int.Parse(val))
            .ToList();
    }

    public bool IsSafe()
    {
        // Determine if the first two numbers in the list are increasing, decreasing, or the same.
        int sign = this.GetSign(this.Levels[0], this.Levels[1]);

        // Check the validity of the list, starting again with the first two values.
        for (int i = 0; i < this.Levels.Count - 1; i++)
        {
            // Determine whether the current value and next value are increasing, decreasing, or the same.
            int currentSign = this.GetSign(this.Levels[i], this.Levels[i + 1]);

            // All lists must be uniformly increasing or decreasing,
            // so return false if the current sign differs from the original sign.
            if (currentSign != sign)
            {
                return false;
            }

            // All lists must change value by 1-3, so make sure the difference is within the expected range.
            int difference = Math.Abs(this.Levels[i] - this.Levels[i + 1]);
            if (difference < 1 || difference > 3)
            {
                return false;
            }
        }

        return true;
    }

    private int GetSign(int firstVal, int secondVal)
    {
        if (firstVal == secondVal)
        {
            return 0;
        }

        return secondVal - firstVal > 0 ? 1 : -1;
    }
}
