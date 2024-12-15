using AdventOfCode2024.Shared;
using System.Diagnostics;

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

    public int FindEasterEgg()
    {
        int i = 0;

        // Unfortunately, the shape of the christmas tree that we're looking for is unknown.
        // To that end, we're going to use a manual brute-force method.
        // In this strategy, we're going to iterate through one step at a time,
        // and after each step, we're going to manually check for any patterns.
        while (true)
        {
            // To help speed up the contiguous shape check, keep track of which spaces have a bot.
            // First, move the bots another step.
            HashSet<Point> locations = new HashSet<Point>();
            foreach (Robot robot in this.robots)
            {
                robot.MoveSteps(1, MAX_X_SIZE, MAX_Y_SIZE);
                locations.Add(robot.Position);
            }

            // Increment the counter.
            i++;

            // Now manually check if the robot's positions form any pattern.
            // string gridString = this.PrintGrid(locations);
            // Debug.WriteLine(gridString);

            // Note: After running the above manual checks, I found that 
            // there were semi-alignments in one direction
            // every 103 steps starting at step 43,
            // and every 101 steps starting at step 68.
            // At this point, we could use another library to find the least common multiple,
            // but I'm just gonna do it in this loop for now.
            if ((i - 43) % MAX_Y_SIZE == 0 && (i - 68) % MAX_X_SIZE == 0)
            {
                break;
            }
        }

        return i;
    }

    /// <summary>
    /// Helper function to visualize the grid.
    /// </summary>
    /// <param name="locations"></param>
    /// <returns></returns>
    private string PrintGrid(HashSet<Point> locations)
    {
        string gridString = "";

        for (int y = 0; y < MAX_Y_SIZE; y++)
        {
            for (int x = 0; x < MAX_X_SIZE; x++)
            {
                if (locations.Contains(new Point(x,y)))
                {
                    gridString += 'x';
                }
                else
                {
                    gridString += '.';
                }
            }

            gridString += '\n';
        }

        return gridString;
    }
}
