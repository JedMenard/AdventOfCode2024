namespace AdventOfCode2024.Shared;

/// <summary>
/// Helper class to encapsulate a grid of points.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Grid<T>
{
    /// <summary>
    /// Private dictionary tracking the values of the grid.
    /// </summary>
    private Dictionary<Point, T?> grid;

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
    /// Static constructor for the common case of reading text from a file.
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static Grid<char> FromStreamReader(StreamReader stream)
    {
        Dictionary<Point, char> grid = new Dictionary<Point, char>();
        int y = 0;
        for (string? line = stream.ReadLine(); line != null; line = stream.ReadLine())
        {
            for (int x = 0; x < line.Length; x++)
            {
                grid[new Point(x, y)] = line[x];
            }

            y++;
        }

        return new Grid<char>(grid);
    }

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
}
