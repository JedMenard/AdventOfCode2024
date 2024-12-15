using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day14;

public class InputParser
{
    private List<Robot> robots;
    private const int MAX_X_SIZE = 101;
    private const int MAX_Y_SIZE = 103;
    private const int STEPS = 100;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day14\\input.txt");

        this.robots = new List<Robot>();
        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine())
        {
            this.robots.Add(new Robot(line));
        }
    }

    public int CountRobotsInQuadrants()
    {
        // Move all the robots 100 steps, then group them by quadrant.
        Dictionary<DirectionEnum, int> countOfRobotsInQuadrants = new Dictionary<DirectionEnum, int>();
        foreach (Robot robot in this.robots)
        {
            // Move the steps.
            robot.MoveSteps(STEPS, MAX_X_SIZE, MAX_Y_SIZE);

            // Determine the quadrant.
            DirectionEnum? quadrant = robot.GetQuadrant(MAX_X_SIZE, MAX_Y_SIZE);

            // Save the quadrant.
            if (quadrant.HasValue)
            {
                countOfRobotsInQuadrants.SumOrInsert(quadrant.Value, 1);
            }
        }

        return countOfRobotsInQuadrants.Aggregate(1, (acc, kvp) => acc * kvp.Value);
    }
}
