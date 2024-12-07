using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day06;

public class InputParser
{
    private Dictionary<Location, LocationMarker> grid;
    private Location startingLocation = new Location(0,0);

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day06\\input.txt");
        this.grid = new Dictionary<Location, LocationMarker>();

        int y = 0;
        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine(), y++)
        {
            for (int x = 0; x < line.Length; x++)
            {
                char v = line[x];
                this.grid[new Location(x, y)] = new LocationMarker(v);
            }
        }
    }

    public int CountTraversedLocations()
    {
        // Traverse the map.
        this.Traverse(this.grid);

        // Return the number of spaces that were traversed.
        return this.grid.Count(kvp => kvp.Value.HasBeenTraversed());
    }

    public int CountPotentialTraversalLoops()
    {
        // Traverse the map once to get the guard's original path.
        Dictionary<Location, LocationMarker> originalPath = this.CopyGrid();
        this.Traverse(originalPath);

        // For each location along the path, place a new obstacle and check again.
        List<Location> potentialObstacles = originalPath
            .Where(kvp => kvp.Value.HasBeenTraversed())
            .Select(kvp => kvp.Key)
            .ToList();

        int loopLocations = 0;
        foreach (Location obstacle in potentialObstacles)
        {
            // Skip the starting location.
            if (obstacle == startingLocation)
            {
                continue;
            }

            // Make a fresh copy of the original grid.
            Dictionary<Location, LocationMarker> grid = this.CopyGrid();

            // Place the obstacle.
            grid[obstacle].Marker = '#';

            // Run the path, and count this location if there's a loop.
            loopLocations += this.Traverse(grid) ? 0 : 1;
        }

        return loopLocations;
    }

    /// <summary>
    /// Traverses the guard through the grid until completion.
    /// Returns an integer representing the number of potential traversal loops.
    /// </summary>
    /// <returns></returns>
    private bool Traverse(Dictionary<Location, LocationMarker> grid)
    {
        // First, figure out where the guard is and which direction she's facing.
        (Location location, LocationMarker startingMarker) = grid.First(kvp => "^>V<".Contains(kvp.Value.Marker));
        DirectionEnum direction = DirectionEnumExtensions.GetDirectionForCharacter(startingMarker.Marker);
        this.startingLocation = new Location(location);

        // Now figure out where the guard is going next.
        Location nextStep = location.GetLocationInDirection(direction, 1);

        // Start patrolling.
        while (grid.ContainsKey(nextStep))
        {
            // Cache some info.
            LocationMarker locationMarker = grid[location];
            LocationMarker nextStepMarker = grid[nextStep];

            // Determine if this is a potential loop.
            if (locationMarker.HasBeenTraversedInDirection(direction))
            {
                // The guard is stuck in a loop, return false.
                return false;
            }

            // Check if the next space forward is blocked.
            if (nextStepMarker.Marker == '#')
            {
                // Next space is blocked.
                // Mark the current space as "traversed" in the original direction,
                // then turn clockwise until we find a free space.
                grid[location].MarkTraversed(direction);
                direction = direction.TurnClockwise();
                grid[location].Marker = direction.GetCharacterForDirection();
                nextStep = location.GetLocationInDirection(direction, 1);
                nextStepMarker = grid[nextStep];
                continue;
            }

            // We are now facing an open direction.
            // Move forward one space and mark the location as traversed.
            locationMarker.Marker = '.';
            locationMarker.MarkTraversed(direction);
            nextStepMarker.Marker = direction.GetCharacterForDirection();

            location = nextStep;
            nextStep = location.GetLocationInDirection(direction, 1);
        }

        // The guard is now leaving the map.
        // Change the character for her current position to an X,
        // and mark the location as traversed.
        grid[location].Marker = '.';
        grid[location].MarkTraversed(direction);

        // The guard made it out of the map, return true.
        return true;
    }

    /// <summary>
    /// Copies the values of a grid into a new grid.
    /// </summary>
    /// <returns></returns>
    private Dictionary<Location, LocationMarker> CopyGrid()
    {
        Dictionary<Location, LocationMarker> gridCopy = new Dictionary<Location, LocationMarker>();

        foreach (KeyValuePair<Location, LocationMarker> kvp in this.grid)
        {
            Location location = kvp.Key;
            LocationMarker marker = kvp.Value;

            Location locationCopy = new Location(location);
            LocationMarker markerCopy = new LocationMarker(marker);
            gridCopy[locationCopy] = markerCopy;
        }

        return gridCopy;
    }

    private string GetGridMap(Dictionary<Location, LocationMarker> grid)
    {
        int maxX = grid.Max(kvp => kvp.Key.X);
        int maxY = grid.Max(kvp => kvp.Key.Y);

        string gridMap = "";
        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                gridMap += grid[new Location(x, y)].Marker;
            }
            gridMap += '\n';
        }

        return gridMap;
    }
}
