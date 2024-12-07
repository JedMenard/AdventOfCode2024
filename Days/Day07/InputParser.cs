namespace AdventOfCode2024.Days.Day07;

public class InputParser
{
    private List<Equation> equations;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day07\\input.txt");
        this.equations = new List<Equation>();

        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine())
        {
            // Each line of input is in the format "result: value value value"...
            // First, parse out the result.
            string[] inputStrings = line.Split(":");

            if (inputStrings.Length != 2)
            {
                throw new ArgumentException($"Invalid input line: {line}");
            }

            // Parse the results into ints.
            long result = long.Parse(inputStrings[0]);
            string equationString = inputStrings[1].Trim();
            List<long> values = equationString.Split(" ").Select(long.Parse).ToList();

            // Add the equation to the list.
            this.equations.Add(new Equation(result, values));
        }
    }

    public long SumValidEquations()
    {
        return this.equations.Where(eq => eq.IsSolveable())
            .Sum(eq => eq.Result);
    }
}
