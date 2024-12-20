﻿using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day06;

public class InputParser
{
    private Dictionary<Point, char> grid;
    private Point startingLocation = new Point(0,0);

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day06\\input.txt");
        this.grid = new Dictionary<Point, char>();

        int y = 0;
        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine(), y++)
        {
            for (int x = 0; x < line.Length; x++)
            {
                this.grid[new Point(x, y)] = line[x];
            }
        }
    }

    public int CountTraversedLocations()
    {
        // Traverse the map.
        this.Traverse(this.grid, out Dictionary<Point, HashSet<DirectionEnum>> traversalPath);

        // Return the number of spaces that were traversed.
        return traversalPath.Count(kvp => !kvp.Value.IsNullOrEmpty());
    }

    public int CountPotentialTraversalLoops()
    {
        // Traverse the map once to get the guard's original path.
        Dictionary<Point, char> originalPath = this.CopyGrid();
        Dictionary<Point, HashSet<DirectionEnum>> traversalPath;
        this.Traverse(originalPath, out traversalPath);

        // For each location along the path, place a new obstacle and check again.
        List<Point> potentialObstacles = traversalPath
            .Where(kvp => !kvp.Value.IsNullOrEmpty())
            .Select(kvp => kvp.Key)
            .ToList();

        int loopLocations = 0;
        foreach (Point obstacle in potentialObstacles)
        {
            // Skip the starting location.
            if (obstacle == startingLocation)
            {
                continue;
            }

            // Make a fresh copy of the original grid.
            Dictionary<Point, char> grid = this.CopyGrid();

            // Place the obstacle.
            grid[obstacle] = '#';

            // Run the path, and count this location if there's a loop.
            loopLocations += this.Traverse(grid, out _) ? 0 : 1;
        }

        return loopLocations;
    }

    /// <summary>
    /// Traverses the guard through the grid until completion.
    /// Returns an integer representing the number of potential traversal loops.
    /// </summary>
    /// <returns></returns>
    private bool Traverse(Dictionary<Point, char> grid, out Dictionary<Point, HashSet<DirectionEnum>> traversalPath)
    {
        // Keep track of where we've been and which direction we've traversed.
        traversalPath = new Dictionary<Point, HashSet<DirectionEnum>>();

        // First, figure out where the guard is and which direction she's facing.
        (Point location, char startingMarker) = grid.First(kvp => "^>V<".Contains(kvp.Value));
        DirectionEnum direction = DirectionEnumExtensions.GetDirectionForCharacter(startingMarker);
        this.startingLocation = new Point(location);

        // Now figure out where the guard is going next.
        Point nextStep = location.GetNextPointInDirection(direction, 1);

        // Start patrolling.
        while (grid.ContainsKey(nextStep))
        {
            // Cache some info.
            char locationMarker = grid[location];
            char nextStepMarker = grid[nextStep];

            // Determine if this is a potential loop
            // by checking if we've already been in this space going in this direction.
            if (traversalPath.GetValueOrDefault(location)?.Contains(direction) ?? false)
            {
                // The guard is stuck in a loop, return false.
                return false;
            }

            // Check if the next space forward is blocked.
            if (nextStepMarker == '#')
            {
                // Next space is blocked.
                // Mark the current space as "traversed" in the original direction,
                // then turn clockwise until we find a free space.
                traversalPath.AddToSetOrInsert(location, direction);
                direction = direction.TurnClockwise();
                grid[location] = direction.GetCharacterForDirection();
                nextStep = location.GetNextPointInDirection(direction, 1);
                nextStepMarker = grid[nextStep];
                continue;
            }

            // We are now facing an open direction.
            // Move forward one space and mark the location as traversed.
            locationMarker = '.';
            traversalPath.AddToSetOrInsert(location, direction);
            nextStepMarker = direction.GetCharacterForDirection();

            location = nextStep;
            nextStep = location.GetNextPointInDirection(direction, 1);
        }

        // The guard is now leaving the map.
        // Change the character for her current position to an X,
        // and mark the location as traversed.
        grid[location] = '.';
        traversalPath.AddToSetOrInsert(location, direction);

        // The guard made it out of the map, return true.
        return true;
    }

    /// <summary>
    /// Copies the values of a grid into a new grid.
    /// </summary>
    /// <returns></returns>
    private Dictionary<Point, char> CopyGrid()
    {
        Dictionary<Point, char> gridCopy = new Dictionary<Point, char>();

        foreach (KeyValuePair<Point, char> kvp in this.grid)
        {
            Point locationCopy = new Point(kvp.Key);
            gridCopy[locationCopy] = kvp.Value;
        }
        return new Dictionary<Point, char>(this.grid);
    }

    /// <summary>
    /// Small helper to print the grid in a user-friendly string.
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    private string GetGridMap(Dictionary<Point, char> grid)
    {
        int maxX = grid.Max(kvp => kvp.Key.X);
        int maxY = grid.Max(kvp => kvp.Key.Y);

        string gridMap = "";
        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                gridMap += grid[new Point(x, y)];
            }
            gridMap += '\n';
        }

        return gridMap;
    }
}
