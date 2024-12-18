using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day18;

public class InputParser
{
    private Grid<char> grid;
    private Grid<int?> dijkstraMap;
    private Point startPoint = new Point(0, 0);
    private Point endPoint = new Point(70, 70);

    public InputParser(int linesToCount)
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day18\\input.txt");

        // Start with a default grid.
        this.grid = new Grid<char>(70, 70, '.');

        // Now read the requested number of lines into walls on the grid.
        for (int i = 0; i < linesToCount; i++)
        {
            // Read the line and make sure it's not empty.
            string? pointString = inputFile.ReadLine();
            if (pointString.IsNullOrEmpty())
            {
                throw new ArgumentException("Ran out of lines before reading the requested number.");
            }

            // Parse the line into ints.
            List<int> pointValues = pointString.Split(",").Select(int.Parse).ToList();

            if (pointValues.Count != 2)
            {
                throw new ArgumentException("Unexpected input line: " + pointString);
            }

            // Save the wall to the grid.
            this.grid[new Point(pointValues[0], pointValues[1])] = '#';
        }

        this.dijkstraMap = this.grid.BuildDijkstraMap(this.startPoint);
    }

    public int GetMinStepsToEnd()
    {
        if (!this.dijkstraMap.PointIsValid(this.endPoint)
            || !this.dijkstraMap.PointIsFilled(this.endPoint))
        {
            throw new Exception("Could not find a path to the end.");
        }

        return this.dijkstraMap[this.endPoint].Value;
    }
}
