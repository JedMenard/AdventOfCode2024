﻿namespace AdventOfCode2024.Shared;

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

    public static DirectionEnum GetDirectionToPoint(this Point first, Point second)
    {
        if (!first.GetAdjacentPoints().Contains(second))
        {
            throw new ArgumentException("Points must be adjacent");
        }

        if (first == second)
        {
            throw new ArgumentException("Points must be distinct");
        }

        if (first.X > second.X)
        {
            return DirectionEnum.West;
        }
        else if (first.X < second.X)
        {
            return DirectionEnum.East;
        }
        else if (first.Y > second.Y)
        {
            return DirectionEnum.North;
        }
        else
        {
            return DirectionEnum.South;
        }
    }

    /// <summary>
    /// Calculates the number of steps required to reach the given point.
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static int DistanceToPoint(this Point first, Point second)
    {
        return Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);
    }
}
