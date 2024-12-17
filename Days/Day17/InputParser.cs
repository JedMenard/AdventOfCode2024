using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day17;

public class InputParser
{
    private Computer computer;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day17\\input.txt");

        int[] registers = new int[3];
        int i = 0;
        string? line = inputFile.ReadLine();

        // Registers come first in the input.
        while (!line.IsNullOrEmpty())
        {
            if (!line.StartsWith("Register"))
            {
                throw new ArgumentException("Invalid input file. Registers must come first.");
            }

            registers[i++] = int.Parse(line.Substring("Register X: ".Length));

            line = inputFile.ReadLine();
        }

        // The registers and program are separated by an empty line.
        // We've already hit the empty line though, thanks to the loop above.
        // All that's left is the program.
        string program = inputFile.ReadLine().Substring("Program: ".Length);

        this.computer = new Computer(registers[0], registers[1], registers[2], program);
    }

    public string ExecuteProgram()
    {
        return this.computer.ExecuteProgram();
    }
}
