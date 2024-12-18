using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day18;

public class InputParser
{
    private List<Point> walls;
    private Point startPoint = new Point(0, 0);
    private Point endPoint = new Point(70, 70);

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day18\\input.txt");
        this.walls = new List<Point>();
        string? line = inputFile.ReadLine();

        // Read the full input into a list of wall locations.
        while (!line.IsNullOrEmpty())
        {
            // Parse the line into ints.
            List<int> pointValues = line.Split(",").Select(int.Parse).ToList();

            if (pointValues.Count != 2)
            {
                throw new ArgumentException("Unexpected input line: " + line);
            }

            // Save the wall to the grid.
            this.walls.Add(new Point(pointValues[0], pointValues[1]));

            // Update the loop variable.
            line = inputFile.ReadLine();
        }
    }

    public int? GetMinStepsToEnd(int wallsToPlace)
    {
        // Fetch the grid.
        Grid<char> grid = this.PopulateGridWithWalls(wallsToPlace);

        // Build a Dijkstra map from the grid.
        Grid<int?> dijkstraMap = grid.BuildDijkstraMap(this.startPoint);

        // Double-check that the end point is within bounds.
        if (!dijkstraMap.PointIsValid(this.endPoint))
        {
            throw new Exception("Invalid end point, out of bounds.");
        }

        return dijkstraMap[this.endPoint];
    }

    public Point FindFirstBlockingWall()
    {
        // We already know that there is a valid path after 1024 steps.
        // Start there, and keep going until we place a new wall along the shortest wall.
        Grid<char> grid = this.PopulateGridWithWalls(1024);
        Grid<int?> dijkstraMap = grid.BuildDijkstraMap(this.startPoint);
        List<Point> pathToEnd = dijkstraMap.GetShortestPathFromDijkstaMap(this.startPoint, this.endPoint);

        Point nextWall = new Point(0,0);
        for (int i = 1025; i < this.walls.Count; i++)
        {
            nextWall = this.walls[i];

            // Check if the wall being placed is along the path to the end.
            if (!pathToEnd.Contains(nextWall))
            {
                // This wall does not interfere with our path.
                // Nothing to do here, check the next wall.
                continue;
            }

            // The wall being placed is along our path.
            // Recompute the path, and if there is none,
            // then we've found the end of the problem.
            grid = this.PopulateGridWithWalls(i);
            dijkstraMap = grid.BuildDijkstraMap(this.startPoint);

            // Check if there's a path here.
            if (!dijkstraMap.PointIsFilled(this.endPoint))
            {
                // No path to the end, so we've found the first blocking wall.
                break;
            }

            // There is a path to the end, so update our collection and keep going.
            pathToEnd = dijkstraMap.GetShortestPathFromDijkstaMap(this.startPoint, this.endPoint);
        }

        return nextWall;
    }

    private Grid<char> PopulateGridWithWalls(int indexOfLastWall)
    {
        // Start with an empty grid.
        Grid<char> grid = new Grid<char>(70, 70, '.');

        // Place the requested number of walls.
        for (int i = 0; i <= indexOfLastWall; i++)
        {
            Point wall = this.walls[i];
            grid[wall] = '#';
        }

        return grid;
    }

    private string DisplayGridWithPath(Grid<char> grid, List<Point> path)
    {
        Grid<char> gridCopy = new Grid<char>(grid);

        foreach (Point point in path)
        {
            gridCopy[point] = 'O';
        }

        return gridCopy.ToString();
    }
}
