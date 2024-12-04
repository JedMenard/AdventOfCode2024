namespace AdventOfCode2024.Days.Day04;

public enum DirectionEnum
{
    North,
    Northeast,
    East,
    Southeast,
    South,
    Southwest,
    West,
    Northwest
}

public static class DirectionEnumExtensions
{
    /// <summary>
    /// Returns a signed integer representing which North-South direction is being faced.
    /// This assumes the zero coordinate is the Northwestern most point.
    /// If the direction is a Northern direction, returns -1.
    /// If the direction is a Southern direction, returns 1.
    /// If the direction is strictly East or West, returns 0.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static int VerticalSign(this DirectionEnum direction)
    {
        if (direction == DirectionEnum.East || direction == DirectionEnum.West)
        {
            return 0;
        }

        if (direction == DirectionEnum.Northwest
            || direction == DirectionEnum.North
            || direction == DirectionEnum.Northeast)
        {
            return -1;
        }

        return 1;
    }


    /// <summary>
    /// Returns a signed integer representing which East-West direction is being faced.
    /// This assumes the zero coordinate is the Northwestern most point.
    /// If the direction is an Eastern direction, returns 1.
    /// If the direction is a Western direction, returns -1.
    /// If the direction is strictly North or South, returns 0.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static int HorizontalSign(this DirectionEnum direction)
    {
        if (direction == DirectionEnum.North || direction == DirectionEnum.South)
        {
            return 0;
        }

        if (direction == DirectionEnum.Northeast
            || direction == DirectionEnum.East
            || direction == DirectionEnum.Southeast)
        {
            return 1;
        }

        return -1;
    }
}
