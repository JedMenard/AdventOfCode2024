using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day11;

public class InputParser
{
    private List<long> rockValues;

    private Dictionary<long, long> rocksWithCounts;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day11\\input.txt");

        string? line = inputFile.ReadLine();

        if (line == null)
        {
            throw new ArgumentException("Input file was empty.");
        }

        // Split the line, parse each value into longs, and provide them a count of 1.
        this.rocksWithCounts = line.Split(" ")
            .Select(long.Parse)
            .Select(val => new KeyValuePair<long, long>(val, 1))
            .ToDictionary();
    }

    public long CountRocksAfterBlinks(int blinks)
    {
        for (int i = 0; i < blinks; i++)
        {
            // Instead of tracking every individual rock,
            // group them by their value and keep track of how many have that value.
            Dictionary<long, long> newRocksWithCounts = new Dictionary<long, long>();

            // Blink all the rocks.
            foreach ((long rockValue, long count) in this.rocksWithCounts)
            {
                // First, find the new rocks for this value.
                List<long> newRocks = this.Blink(rockValue);

                // Add them to the new set.
                newRocks.ForEach(newRock => newRocksWithCounts.SumOrInsert(newRock, count));
            }

            // Save the new set.
            this.rocksWithCounts = newRocksWithCounts;
        }

        // Sum the count of rocks and return.
        return this.rocksWithCounts.Values.Sum();
    }

    /// <summary>
    /// Determines the result of blinking for the provided rock value.
    /// If the value is 0, increments to 1 and returns.
    /// If the value has an even number of digits, splits the digits
    /// and returns the left and right halves as new rocks.
    /// Otherwise, multiplies the rock value by 2024 and returns.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private List<long> Blink(long value)
    {
        // If the rock has value 0, then it gets updated to one.
        if (value == 0)
        {
            value = 1;
            return new List<long> { value };
        }

        // If the value has an even number of digits,
        // split the digits in half and return two rocks.
        string valueAsString = value.ToString();
        int length = valueAsString.Length;
        if (length % 2 == 0)
        {
            // Find out how long each new number is.
            int halfLength = length / 2;

            // Find the new numbers.
            long leftValue = long.Parse(valueAsString.Substring(0, halfLength));
            long rightValue = long.Parse(valueAsString.Substring(halfLength, halfLength));

            // Create two new rocks and return them.
            return new List<long> { leftValue, rightValue };
        }

        // If none of the above rules apply, multiply the value by 2024.
        value *= 2024;
        return new List<long> { value };
    }
}
