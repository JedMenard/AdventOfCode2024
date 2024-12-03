using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days.Day03;

public class InputParser
{
    /// <summary>
    /// Matches the pattern "mul(###,###)"
    /// </summary>
    private static Regex multiplyPattern = new Regex(@"mul\([0-9]{1,3},[0-9]{1,3}\)");

    /// <summary>
    /// Matches the patterns "do()" and "don't()"
    /// </summary>
    private static Regex enablePattern = new Regex(@"do(n't)?\(\)");

    private List<Command> Commands;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\DayThree\\input.txt");
        this.Commands = new List<Command>();

        // Parse the results using regex.
        string? line = inputFile.ReadLine();
        int lineNumber = 0;
        while (line != null)
        {
            // Add both the mult and enable/disable commands.
            this.Commands.AddRange(this.ParseMultCommands(line, lineNumber));
            this.Commands.AddRange(this.ParseEnableCommands(line, lineNumber));

            // Increment the loop.
            line = inputFile.ReadLine();
            lineNumber++;
        }

        // Sort the results by line number and index.
        this.Commands = this.Commands.OrderBy(command => command.LineNumber)
            .ThenBy(command => command.Index)
            .ToList();

        return;
    }

    /// <summary>
    /// Parses the commands and retuns the result of summing the commands
    /// </summary>
    /// <returns></returns>
    public int GetResult()
    {
        bool isEnabled = true;
        int sum = 0;

        // Iterate over the commands, which should now be in the correct order.
        foreach (Command command in this.Commands)
        {
            // First, check for Toggle commands and set the enabled state as appropriate.
            if (command is ToggleEnableCommand toggleCommand)
            {
                isEnabled = toggleCommand.Enabled;
            }

            // Now process mult commands if enabled.
            else if (isEnabled && command is MultCommand multCommand)
            {
                sum += multCommand.Result;
            }
        }

        return sum;
    }

    /// <summary>
    /// Parses the input and retuns the list of found do() and don't() commands.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="lineNumber"></param>
    /// <returns></returns>
    private List<MultCommand> ParseMultCommands(string input, int lineNumber)
    {
        return multiplyPattern.Matches(input)
                .Select(result => new MultCommand(result.Value, lineNumber, result.Index))
                .ToList();
    }

    /// <summary>
    /// Parses the input and returns the list of found mult() commands.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="lineNumber"></param>
    /// <returns></returns>
    private List<ToggleEnableCommand> ParseEnableCommands(string input, int lineNumber)
    {
        return enablePattern.Matches(input)
                .Select(result => new ToggleEnableCommand(result.Value, lineNumber, result.Index))
                .ToList();
    }
}
