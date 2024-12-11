using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day10;

public class InputParser
{
    private Grid<int> grid;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day10\\input.txt");

        Dictionary<Point, int> gridDictionary = new Dictionary<Point, int>();

        int y = 0;

        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine())
        {
            for (int x = 0; x < line.Length; x++)
            {
                gridDictionary[new Point(x, y)] = int.Parse(line[x].ToString());
            }

            y++;
        }

        this.grid = new Grid<int>(gridDictionary);
    }

    public int SumTrailheadScores()
    {
        List<Point> trailheads = this.GetTrailheads();

        return trailheads.Sum(this.GetRating);
    }

    /// <summary>
    /// Returns the number of distinct paths that lead to a peak from the given point.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    private int GetRating(Point point)
    {
        int height = this.grid[point];

        // Base case: We're at the end of the trail, return one.
        if (height == 9)
        {
            return 1;
        }

        // Otherwise, check each direction and sum the results.
        int score = 0;
        foreach (DirectionEnum direction in DirectionEnumExtensions.CardinalDirections())
        {
            Point nextPoint = point.GetNextPointInDirection(direction);

            // Determine if the next point is just one step up.
            if (!this.grid.PointIsValid(nextPoint) || this.grid[nextPoint] != height + 1)
            {
                // This point isn't valid, skip it.
                continue;
            }

            // This path is valid, get the score.
            score += this.GetRating(nextPoint);
        }

        return score;
    }

    /// <summary>
    /// Returns the locations of all trailheads on the grid.
    /// </summary>
    /// <returns></returns>
    private List<Point> GetTrailheads()
    {
        return this.grid.AsEnumerable.Where(kvp => kvp.Value == 0)
            .Select(kvp => kvp.Key)
            .ToList();
    }
}
