using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day15;

public class InputParser
{
    private Robot robot;
    private Grid<char> grid;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day15\\input.txt");

        // The input file is separated into two parts:
        // the grid first, and the robot's moves second.
        // First, parse out the grid.
        List<string> gridInput = new List<string>();
        string? line = inputFile.ReadLine();
        while (!line.IsNullOrEmpty())
        {
            gridInput.Add(line);
            line = inputFile.ReadLine();
        }

        this.grid = Grid<char>.FromStrings(gridInput);

        // Now find the robot's starting position.
        Point robotStart = grid.AsEnumerable.First(kvp => kvp.Value == '@').Key;

        // Lastly, create the robot.
        line = inputFile.ReadLine();

        if (line == null)
        {
            throw new ArgumentException("No input line representing the robot's moves.");
        }

        List<string> moveStrings = new List<string>();

        while (!line.IsNullOrEmpty())
        {
            moveStrings.Add(line);
            line = inputFile.ReadLine();
        }

        this.robot = new Robot(moveStrings, robotStart);
    }

    public int SumCoordinatesAfterMoves()
    {
        // Move the robot.
        this.robot.MoveToCompletion(this.grid);

        // Sum the coordinates.
        return this.grid.AsEnumerable
            .Where(kvp => kvp.Value == 'O')
            .Select(kvp => kvp.Key)
            .Sum(point => 100 * point.Y + point.X);
    }
}
