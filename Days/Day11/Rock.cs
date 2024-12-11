namespace AdventOfCode2024.Days.Day11;

public class Rock
{
    public long Value;

    public Rock(long value)
    {
        this.Value = value;
    }

    public List<Rock> Blink()
    {
        // If the rock has value 0, then it gets updated to one.
        if (this.Value == 0)
        {
            this.Value = 1;
            return new List<Rock> { this };
        }

        // If the value has an even number of digits,
        // split the digits in half and return two rocks.
        string valueAsString = this.Value.ToString();
        int length = valueAsString.Length;
        if (length % 2 == 0)
        {
            // Find out how long each new number is.
            int halfLength = length / 2;

            // Find the new numbers.
            long leftValue = long.Parse(valueAsString.Substring(0, halfLength));
            long rightValue = long.Parse(valueAsString.Substring(halfLength, halfLength));

            // Create two new rocks and return them.
            return new List<Rock> { new Rock(leftValue), new Rock(rightValue) };
        }

        // If none of the above rules apply, multiply the value by 2024.
        this.Value *= 2024;
        return new List<Rock> { this };
    }

    public override string ToString() => this.Value.ToString();
}
