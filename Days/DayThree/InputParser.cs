using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days.DayThree;

public class InputParser
{
    /// <summary>
    /// Matches the pattern "mul(###,###)"
    /// </summary>
    private Regex multiplyPattern = new Regex(@"mul\([0-9]{1,3},[0-9]{1,3}\)");
    private List<string> Commands;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\DayThree\\input.txt");
        this.Commands = new List<string>();

        // Parse the results using regex.
        string? line = inputFile.ReadLine();
        while (line != null)
        {
            this.Commands.AddRange(this.multiplyPattern.Matches(line)
                .Select(result => result.Value)
                .ToList());
            line = inputFile.ReadLine();
        }

        return;
    }

    /// <summary>
    /// Parses the commands and retuns the result of summing the commands
    /// </summary>
    /// <returns></returns>
    public int GetResult()
    {
        // First, strip each command of its "mul" and parenthesis, leaving just the comma-separated values.
        List<string> csvs = this.Commands.Select(cmd => cmd.Substring(4, cmd.Length - 5)).ToList();

        // Now split them and cast them to ints.
        List<int[]> values = csvs
            .Select(csv => csv.Split(",")
                .Select(str => int.Parse(str))
                .ToArray()
            ).ToList();

        // Finally, multiply the numbers and sum them up.
        return values.Sum(val => val[0] * val[1]);
    }
}
