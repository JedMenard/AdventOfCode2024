using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day14;

public class Robot
{
    public Point Position;
    private int xVelocity;
    private int yVelocity;

    public Robot(string input)
    {
        string[] inputStrings = input.Split(" ");

        // Verify that there are exactly two parameters 
        if (inputStrings.Length != 2)
        {
            throw new ArgumentException("Unexpected input string: " + input);
        }

        string pointString = inputStrings[0].Substring(2);
        string velocityString = inputStrings[1].Substring(2);

        int[] pointValues = pointString.Split(",").Select(int.Parse).ToArray();
        int[] velocityValues = velocityString.Split(",").Select(int.Parse).ToArray();

        if (pointValues.Length != 2 || velocityValues.Length != 2)
        {
            throw new ArgumentException("Starting point and velocity should both have x and y values.");
        }

        this.Position = new Point(pointValues[0], pointValues[1]);
        this.xVelocity = velocityValues[0];
        this.yVelocity = velocityValues[1];
    }

    /// <summary>
    /// Moves the robot the given number of steps and returns the new position.
    /// </summary>
    /// <param name="steps"></param>
    /// <param name="maxX"></param>
    /// <param name="maxY"></param>
    /// <returns></returns>
    public Point MoveSteps(int steps, int maxX, int maxY)
    {
        int xPosition = (this.Position.X + (this.xVelocity * steps)) % maxX;
        int yPosition = (this.Position.Y + (this.yVelocity * steps)) % maxY;

        // Modulous math does not remove negative numbers, so handle those ourselves.
        if (xPosition < 0)
        {
            xPosition += maxX;
        }

        if (yPosition < 0)
        {
            yPosition += maxY;
        }

        this.Position = new Point(xPosition, yPosition);
        return this.Position;
    }

    /// <summary>
    /// Returns the direction representing which quandrant this robot is in.
    /// If the robot is on a midline (e.g. not in a quadrant), returns null.
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="maxY"></param>
    /// <returns></returns>
    public DirectionEnum? GetQuadrant(int maxX, int maxY)
    {
        int midX = (maxX - 1) / 2;
        int midY = (maxY - 1) / 2;

        // Check if we're on a midline.
        if (this.Position.X == midX || this.Position.Y == midY)
        {
            // Robots are only considered to be in a quadrant if they're not on a midline.
            return null;
        }

        bool leftHalf = this.Position.X < midX;
        bool upperHalf = this.Position.Y < midY;

        if (leftHalf)
        {
            return upperHalf ? DirectionEnum.Northwest : DirectionEnum.Southwest;
        }
        else
        {
            return upperHalf ? DirectionEnum.Northeast : DirectionEnum.Southeast    ;
        }
    }
}
