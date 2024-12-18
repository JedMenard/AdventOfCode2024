namespace AdventOfCode2024.Shared;

public static class CharGridExtensions
{
    /// <summary>
    /// Builds a map representing the minimum number of valid steps
    /// required to reach each point in the grid.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="startPoint"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Grid<int?> BuildDijkstraMap(this Grid<char> grid, Point startPoint)
    {
        // Start with an empty map.
        int maxX = grid.Keys.MaxBy(point => point.X).X;
        int maxY = grid.Keys.MaxBy(point => point.Y).Y;

        Grid<int?> dijkstraMap = new Grid<int?>(maxX, maxY, null);

        // Recursively build the map.
        buildDijkstraMap(grid, dijkstraMap, startPoint, 0);

        // Return the result.
        return dijkstraMap;
    }

    /// <summary>
    /// Recursively builds a Dijkstra map from the given input.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="dijkstraMap"></param>
    /// <param name="currentLocation"></param>
    /// <param name="steps"></param>
    private static void buildDijkstraMap(Grid<char> grid,
        Grid<int?> dijkstraMap,
        Point currentLocation,
        int steps)
    {
        // First, check if we've already been here from a more efficient path.
        if (dijkstraMap.PointIsFilled(currentLocation)
            && dijkstraMap[currentLocation] <= steps)
        {
            return;
        }

        // This is either a new location, or a more efficient path.
        // Either way, set the distance to our current step count.
        dijkstraMap[currentLocation] = steps;

        // Now determine all valid next steps.
        foreach (Point nextLocation in currentLocation.GetAdjacentPoints())
        {
            // Check if this point is an open space on the grid.
            if (grid.PointIsValid(nextLocation) && grid[nextLocation] != '#')
            {
                // This space is valid. Check it.
                buildDijkstraMap(grid, dijkstraMap, nextLocation, steps + 1);
            }

            // The point is either a wall or off the map, so there's nothing left to do.
        }
    }
}
