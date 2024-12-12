namespace AdventOfCode2024.Shared;

public enum DirectionEnum
{
    North = 0,
    Northeast = 1,
    East = 2,
    Southeast = 3,
    South = 4,
    Southwest = 5,
    West = 6,
    Northwest =  7
}

public static class DirectionEnumExtensions
{
    /// <summary>
    /// All cardinal directions.
    /// </summary>
    /// <returns></returns>
    public static List<DirectionEnum> CardinalDirections => new List<DirectionEnum>
    {
        DirectionEnum.North,
        DirectionEnum.East,
        DirectionEnum.South,
        DirectionEnum.West
    };

    /// <summary>
    /// All diagonal cardinal directions.
    /// </summary>
    public static List<DirectionEnum> DiagonalDirections => new List<DirectionEnum>
    {
        DirectionEnum.Northeast,
        DirectionEnum.Southeast,
        DirectionEnum.Southwest,
        DirectionEnum.Northwest
    };

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

    /// <summary>
    /// Returns the direction that is <paramref name="steps"/> steps in the clockwise direction.
    /// Defaults to a 90 degree turn.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="steps"></param>
    /// <returns></returns>
    public static DirectionEnum TurnClockwise(this DirectionEnum direction, int steps = 2)
    {
        // Use modulus math to simplify the logic.
        return (DirectionEnum)((int)(direction + steps) % 8);
    }

    /// <summary>
    /// Turns a character into a direction enum.
    /// Must be one of the following: ^ > V <
    /// </summary>
    /// <param name="directionCharacter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static DirectionEnum GetDirectionForCharacter(char directionCharacter)
    {
        switch (directionCharacter)
        {
            case '^':
                return DirectionEnum.North;
            case '>':
                return DirectionEnum.East;
            case 'V':
                return DirectionEnum.South;
            case '<':
                return DirectionEnum.West;
            default:
                throw new ArgumentException($"Invalid direction character: {directionCharacter}");
        }
    }

    /// <summary>
    /// Turns a direction into a character.
    /// Only the four cardinal directions have corresponding characters.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static char GetCharacterForDirection(this DirectionEnum direction)
    {
        switch (direction)
        {
            case DirectionEnum.North:
                return '^';
            case DirectionEnum.East:
                return '>';
            case DirectionEnum.South:
                return 'V';
            case DirectionEnum.West:
                return '<';
            default:
                throw new ArgumentException("Only the four cardinal directions have characters.");
        }
    }
}
