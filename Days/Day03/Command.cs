namespace AdventOfCode2024.Days.Day03;

/// <summary>
/// Simple helper class to track a command and it's position in the command string.
/// </summary>
public class Command
{
    public string RawValue;
    public int LineNumber;
    public int Index;

    public Command(string rawValue, int lineNumber, int index)
    {
        this.RawValue = rawValue;
        this.LineNumber = lineNumber;
        this.Index = index;
    }
}
