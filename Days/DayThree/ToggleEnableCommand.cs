namespace AdventOfCode2024.Days.DayThree;

public class ToggleEnableCommand : Command
{
    public bool Enabled;

    public ToggleEnableCommand(string rawValue, int lineNumber, int index) : base(rawValue, lineNumber, index)
    {
        this.Enabled = rawValue == "do()";
    }
}
