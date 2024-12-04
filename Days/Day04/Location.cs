namespace AdventOfCode2024.Days.Day04;

public class Location
{
    public int X;
    public int Y;

    public Location(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Gets the location point in the given direction the given number of steps away.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    public Location GetLocationInDirection(DirectionEnum direction, int steps)
    {
        int newX = this.X + (direction.HorizontalSign() * steps);
        int newY = this.Y + (direction.VerticalSign() * steps);

        return new Location(newX, newY);
    }

    // Comparator override for dictionary hashing.
    public override bool Equals(object? obj)
    {
        if (obj is Location other) {
            return this.X == other.X && this.Y == other.Y;
        }

        return false;
    }

    // Comparator override for dictionary hashing.
    public override int GetHashCode()
    {
        return (this.X * 10000 + this.Y).GetHashCode();
    }
}
