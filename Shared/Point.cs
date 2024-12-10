namespace AdventOfCode2024.Shared;

public class Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(Point other)
    {
        this.X = other.X;
        this.Y = other.Y;
    }

    /// <summary>
    /// Gets the point in the given direction the given number of steps away.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    public Point GetNextPointInDirection(DirectionEnum direction, int steps = 1)
    {
        int newX = X + direction.HorizontalSign() * steps;
        int newY = Y + direction.VerticalSign() * steps;

        return new Point(newX, newY);
    }

    // Comparator override for dictionary hashing.
    public override bool Equals(object? obj)
    {
        if (obj is Point other)
        {
            return this == other;
        }

        return false;
    }

    public static bool operator== (Point first, Point second)
    {
        return first.X == second.X && first.Y == second.Y;
    }

    public static bool operator!= (Point first, Point second)
    {

        return first.X != second.X || first.Y != second.Y;
    }

    public override string ToString()
    {
        return $"X: {this.X}, Y: {this.Y}";
    }

    // Comparator override for dictionary hashing.
    public override int GetHashCode()
    {
        return HashCode.Combine(this.X, this.Y);
    }

}
