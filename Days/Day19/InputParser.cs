using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day19;

public class InputParser
{
    private Onsen onsen;
    private List<string> patterns = new List<string>();

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day19\\input.txt");

        string? line = inputFile.ReadLine();

        if (line.IsNullOrEmpty())
        {
            throw new ArgumentException("Input file is empty");
        }

        this.onsen = new Onsen(line);

        // Parse in the empty line, then parse in the first pattern.
        line = inputFile.ReadLine();
        line = inputFile.ReadLine();

        while (!line.IsNullOrEmpty())
        {
            this.patterns.Add(line);
            line = inputFile.ReadLine();
        }
    }

    public int SumSolutionCounts()
    {
        return this.patterns.Sum(pattern => this.onsen.GetAllSolutions(pattern).Count);
    }
}
