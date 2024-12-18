using System.Collections;
using System.IO;

namespace AdventOfCode2024.Shared;

/// <summary>
/// Helper class to encapsulate a grid of points.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Grid<T> : IEnumerable<KeyValuePair<Point, T>>
{
    #region Properties

    /// <summary>
    /// Private dictionary tracking the values of the grid.
    /// </summary>
    private Dictionary<Point, T?> grid;

    /// <summary>
    /// Returns the area of the grid.
    /// </summary>
    /// <returns></returns>
    public int Area => this.grid.Keys.Count;

    /// <summary>
    /// Returns the perimeter of the grid.
    /// </summary>
    /// <returns></returns>
    public int Perimeter => this.Keys.Sum(point => point.GetAdjacentPoints().Count(next => !this.PointIsValid(next)));

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor for an empty grid.
    /// </summary>
    public Grid()
    {
        this.grid = new Dictionary<Point, T?>();
    }

    /// <summary>
    /// Constructor from a pre-calculated dictionary.
    /// </summary>
    /// <param name="grid"></param>
    public Grid(Dictionary<Point, T?> grid)
    {
        this.grid = grid;
    }

    /// <summary>
    /// Copy-constructor.
    /// </summary>
    /// <param name="other"></param>
    public Grid(Grid<T> other)
    {
        this.grid = new();

        foreach ((Point point, T? value) in other.grid)
        {
            this.grid[new Point(point)] = value;
        }
    }

    /// <summary>
    /// Default-value constructor for a grid of a given size.
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxY"></param>
    /// <param name="defaultValue"></param>
    public Grid(int maxX, int maxY, T defaultValue)
    {
        this.grid = new Dictionary<Point, T?>();

        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x <= maxX; x++)
            {
                this.grid[new Point(x, y)] = defaultValue;
            }
        }
    }

    /// <summary>
    /// Static constructor for the common case of reading text from a file.
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static Grid<char> FromStreamReader(StreamReader stream)
    {
        List<string> gridInput = new List<string>();
        string? line = stream.ReadLine();
        while (!line.IsNullOrEmpty())
        {
            gridInput.Add(line);
            line = stream.ReadLine();
        }

        return FromStrings(gridInput);
    }

    /// <summary>
    /// Static constructor for the common case of reading text from a list of strings.
    /// </summary>
    /// <param name="gridInput"></param>
    /// <returns></returns>
    public static Grid<char> FromStrings(IEnumerable<string> gridInput)
    {
        Dictionary<Point, char> grid = new Dictionary<Point, char>();
        int y = 0;
        foreach (string line in gridInput)
        {
            for (int x = 0; x < line.Length; x++)
            {
                grid[new Point(x, y)] = line[x];
            }

            y++;
        }

        return new Grid<char>(grid);
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Checks if there is a non-null value at the provided point.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public bool PointIsFilled(Point point)
    {
        if (!this.PointIsValid(point))
        {
            // The requested point is invalid.
            throw new ArgumentException($"Invalid point: {point.ToString()}");
        }

        return this.grid.GetValueOrDefault(point) == null ? false : true;
    }

    /// <summary>
    /// Checks if the provided point falls within the grid.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool PointIsValid(Point point)
    {
        // Determine if the point falls within the grid.
        return this.grid.ContainsKey(point);
    }

    /// <summary>
    /// Iterates over the whole grid to find all contiguous regions.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public List<Grid<T>> GetContiguousRegions()
    {
        // Start with an empty collection.
        List<Grid<T>> contiguousRegions = new List<Grid<T>>();

        // Iterate over each point and find all contiguous regions.
        foreach (Point point in this.Keys)
        {
            // If we've already processed this point, skip it.
            if (contiguousRegions.Any(reg => reg.PointIsValid(point)))
            {
                continue;
            }

            // This is a new point for which we have not yet found a contiguous region.
            // Process it, and add the region to the collection.
            contiguousRegions.Add(this.GetContiguousRegion(point));
        }

        return contiguousRegions;
    }

    /// <summary>
    /// Returns a grid representing the contiguous region of similar values around the provided point.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public Grid<T> GetContiguousRegion(Point point)
    {
        return this.GetContiguousRegion(point, new Grid<T>());
    }

    /// <summary>
    /// Recursively traverses a grid from a starting point to find all contiguous points.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="point"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    private Grid<T> GetContiguousRegion(Point point, Grid<T> output)
    {
        T? value = this[point];

        if (value == null)
        {
            throw new ArgumentException("Attempting to find contiguous regions for an empty point on the grid.");
        }

        // First, add the current point to the output.
        output[point] = value;

        // Now determine all potential next points.
        foreach (Point nextPoint in point.GetAdjacentPoints())
        {
            // If the next point is outside the original grid,
            // if there is no value in the original grid at the next point,
            // or if the values are not the same,
            // then skip this point.
            if (!this.PointIsValid(nextPoint)
                || !this.PointIsFilled(nextPoint)
                || !value.Equals(this[nextPoint]))
            {
                continue;
            }

            // This point is a valid contiguous region.
            // If we haven't visited it already, then process it recursively.
            if (!output.PointIsValid(nextPoint))
            {
                output = this.GetContiguousRegion(nextPoint, output);
            }
        }

        // Done processing the grid, return the output.
        return output;
    }

    /// <summary>
    /// Counts the number of distinct sides that the grid has.
    /// </summary>
    /// <returns></returns>
    public int CountSides()
    {
        int sideCount = 0;

        foreach (DirectionEnum direction in DirectionEnumExtensions.CardinalDirections)
        {
            // Instantiate an empty list representing all the points
            // that have a side in the given direction.
            List<Point> sides = new List<Point>();

            // Iterate over the grid.
            foreach (Point point in this.Keys)
            {
                // If the point is a side in the correct direciton, add it to the list.
                if (!this.PointIsValid(point.GetNextPointInDirection(direction)))
                {
                    sides.Add(point);
                }
            }

            // We now have a list representing all side points in the given direction.
            // Order them, so that we can count which ones are contiguous.
            sides = direction.IsHorizontal()
                ? sides.OrderBy(side => side.X).ThenBy(side => side.Y).ToList()
                : sides.OrderBy(side => side.Y).ThenBy(side => side.X).ToList();

            // Get our starting values.
            int sections = 1;
            Point previousPoint = sides[0];

            // We are traversing our lists from top-left to bottom-right.
            DirectionEnum orthogonalDirection = direction.IsHorizontal()
                ? DirectionEnum.South
                : DirectionEnum.East;

            // Count how many contiguous sections there are.
            for (int i = 1; i < sides.Count; i++)
            {
                Point currentPoint = sides[i];

                // If this point is one step in the orthogonal direction from the previous point,
                // then it is considered part of the same side.
                // Otherwise, we've found a new side, so increment the counter.
                if (previousPoint.GetNextPointInDirection(orthogonalDirection) != currentPoint)
                {
                    sections++;
                }

                // Update our loop variable.
                previousPoint = currentPoint;
            }

            // Add the count from this direction to the overall count.
            sideCount += sections;
        }

        return sideCount;
    }

    #endregion

    #region Enumeration

    /// <summary>
    /// Gets a collection containing the keys in the grid.
    /// </summary>
    public IEnumerable<Point> Keys => this.grid.Keys;

    /// <summary>
    /// Gets a collection containing the values in the grid.
    /// </summary>
    public IEnumerable<T?> Values => this.grid.Values;

    /// <summary>
    /// Returns the grid as an iterable collection of KeyValuePairs.
    /// </summary>
    public IEnumerable<KeyValuePair<Point, T?>> AsEnumerable => this.grid.AsEnumerable();

    #endregion

    #region Overrides

    // Accessor override.
    public T? this[Point point]
    {
        get => this.grid[point];
        set => this.grid[point] = value;
    }

    /// <summary>
    /// Helper function to visualize the grid as a string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        int minX = this.grid.Keys.Min(key => key.X);
        int maxX = this.grid.Keys.Max(key => key.X);
        int minY = this.grid.Keys.Min(key => key.Y);
        int maxY = this.grid.Keys.Max(key => key.Y);

        string gridString = "";
        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                gridString += this.grid[new Point(x, y)];
            }

            gridString += '\n';
        }

        return gridString;
    }

    IEnumerator<KeyValuePair<Point, T>> IEnumerable<KeyValuePair<Point, T>>.GetEnumerator() => this.grid.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.grid.GetEnumerator();

    #endregion
}
