using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day17;

public class InputParser
{
    private Computer computer;
    private string programString;

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
        this.programString = inputFile.ReadLine().Substring("Program: ".Length);

        this.computer = new Computer(registers[0], registers[1], registers[2], this.programString);
    }

    public string ExecuteProgram()
    {
        return this.computer.ExecuteProgram();
    }

    public long FindCopyValue()
    {
        string output = "";
        long i = 56368771254;
        long? solution = null;

        HashSet<long> testedNumbers = new HashSet<long>();

        // Loop until we get the output we want.
        while (!solution.HasValue)
        {
            // Increment the counter.
            i += 1;

            // This equation determines the first character output by the computer.
            if ((((i % 8) ^ 4) ^ (long)(i / Math.Pow(2, (i % 8) ^ 1))) % 8 != 2)
            {
                continue;
            }

            // Potential solution, check it.
            output = new Computer(i,
                this.computer.RegisterB,
                this.computer.RegisterC,
                this.programString
            ).ExecuteProgram();

            if (output == this.programString)
            {
                solution = i;
                break;
            }
        }

        return solution.Value;
    }
}
