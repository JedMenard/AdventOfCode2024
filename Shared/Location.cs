namespace AdventOfCode2024.Shared;

public class Location
{
    public int X;
    public int Y;

    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Gets the location point in the given direction the given number of steps away.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    public Location GetLocationInDirection(DirectionEnum direction, int steps)
    {
        int newX = X + direction.HorizontalSign() * steps;
        int newY = Y + direction.VerticalSign() * steps;

        return new Location(newX, newY);
    }

    // Comparator override for dictionary hashing.
    public override bool Equals(object? obj)
    {
        if (obj is Location other)
        {
            return X == other.X && Y == other.Y;
        }

        return false;
    }

    // Comparator override for dictionary hashing.
    public override int GetHashCode()
    {
        return (X * 10000 + Y).GetHashCode();
    }
}
