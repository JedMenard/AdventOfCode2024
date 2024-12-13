namespace AdventOfCode2024.Days.Day13;

public class InputParser
{
    private List<ClawMachine> clawMachines;

    public InputParser()
    {
        this.clawMachines = new List<ClawMachine>();
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day13\\input.txt");

        // Parse the whole file into one string.
        string fullInput = inputFile.ReadToEnd();

        // Each claw machine's input is separated by an empty line,
        // so split on two consecutive newline charachters.
        IEnumerable<string> inputStrings = fullInput.Split("\n\n");

        // Parse each input into a ClawMachine object.
        foreach (string inputString in inputStrings)
        {
            // Split each input and parse it into an object.
            ClawMachine clawMachine = new ClawMachine(inputString.Split("\n").ToList());
            this.clawMachines.Add(clawMachine);
        }
    }

    public int SumTokenCost()
    {
        return this.clawMachines.Sum(claw => claw.GetMinCoinCount());
    }
}
