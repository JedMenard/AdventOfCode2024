namespace AdventOfCode2024.Days.Day03;

public class ToggleEnableCommand : Command
{
    public bool Enabled;

    public ToggleEnableCommand(string rawValue, int lineNumber, int index) : base(rawValue, lineNumber, index)
    {
        this.Enabled = rawValue == "do()";
    }
}
