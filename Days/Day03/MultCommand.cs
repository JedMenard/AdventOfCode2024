namespace AdventOfCode2024.Days.Day03;

public class MultCommand : Command
{
    public int FirstVal;
    public int SecondVal;
    public int Result => this.FirstVal * this.SecondVal;

    public MultCommand(string rawValue, int lineNumber, int index) : base(rawValue, lineNumber, index)
    {
        // First, strip the raw value of the leading and trailing text.
        string strippedValue = rawValue.Substring(4, rawValue.Length - 5);

        // Now split the results and cast to ints.
        int[] values = strippedValue.Split(",")
            .Select(val => int.Parse(val))
            .ToArray();

        // Verify that there are only two values provided.
        if (values.Length != 2)
        {
            throw new ArgumentException("Unexpected values in multiplication command: " + rawValue);
        }

        // Save the results.
        this.FirstVal = values[0];
        this.SecondVal = values[1];
    }
}
