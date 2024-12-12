namespace AdventOfCode2024.Shared;

public static class PointExtensions
{
    /// <summary>
    /// Gets the point in the given direction the given number of steps away.
    /// </summary>
    /// <param name="point"></param>
    /// <param name="direction"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    public static Point GetNextPointInDirection(this Point point, DirectionEnum direction, int steps = 1)
    {
        int newX = point.X + direction.HorizontalSign() * steps;
        int newY = point.Y + direction.VerticalSign() * steps;

        return new Point(newX, newY);
    }

    /// <summary>
    /// Returns a collection of points representing the points <paramref name="steps"/> steps
    /// away from the original point in each of the four cardinal directions.
    /// </summary>
    /// <param name="point"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    public static IEnumerable<Point> GetAdjacentPoints(this Point point, int steps = 1)
    {
        return DirectionEnumExtensions.CardinalDirections
            .Select(direction => point.GetNextPointInDirection(direction, steps));
    }
}
