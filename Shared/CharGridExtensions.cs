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

    /// <summary>
    /// Determines a single shortest path from the start to the end.
    /// </summary>
    /// <param name="dijkstraMap"></param>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static List<Point> GetShortestPathFromDijkstaMap(this Grid<int?> dijkstraMap, Point startPoint, Point endPoint)
    {
        // Make sure there is a valid end point.
        if (!dijkstraMap.PointIsValid(endPoint)
            || !dijkstraMap.PointIsFilled(endPoint))
        {
            throw new ArgumentException("Dijkstra map contains no paths to end point.");
        }

        // Start at the end and work backwards.
        Point currentPoint = endPoint;
        List<Point> path = new List<Point> { endPoint };
        int currentDistance = dijkstraMap[endPoint].Value;

        // Keep going until we're at the start.
        while (currentPoint != startPoint)
        {
            // This method only returns a single path to the end, rather than all paths.
            // To that end, we don't care how many other points next to our current point
            // would lead back to the start, we just need any arbitrary point.
            foreach (Point nextPoint in currentPoint.GetAdjacentPoints())
            {
                // Skip points that are off the map or have no valid paths.
                if (!dijkstraMap.PointIsValid(nextPoint)
                    || !dijkstraMap.PointIsFilled(nextPoint))
                {
                    continue;
                }

                // Check if we're heading in the right direction.
                int nextDistance = dijkstraMap[nextPoint].Value;
                if (nextDistance < currentDistance)
                {
                    // We are! Ignore the remaining points, and move towards this one.
                    path.Add(nextPoint);
                    currentDistance = nextDistance;
                    currentPoint = nextPoint;
                    break;
                }
            }
        }

        // We should now be at the end.
        // Reverse the list so it's in the right order, and return it.
        path.Reverse();
        return path;
    }
}
