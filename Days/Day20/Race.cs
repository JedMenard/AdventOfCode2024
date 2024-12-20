using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day20;

public class Race
{
    private Grid<char> course;
    private Grid<int?> dijkstraMap;
    private List<Point> path;
    private Point startPoint;
    private Point endPoint;
    private int maxCheatSteps;

    public Race(StreamReader input, int maxCheatSteps = 20)
    {
        this.maxCheatSteps = maxCheatSteps;
        this.course = Grid<char>.FromStreamReader(input);

        this.startPoint = this.course.First(kvp => kvp.Value == 'S').Key;
        this.endPoint = this.course.First(kvp => kvp.Value == 'E').Key;

        this.dijkstraMap = this.course.BuildSinglePathDijkstraMap(this.startPoint, out this.path);
    }

    public HashSet<Cheat> FindAllCheats()
    {
        HashSet<Cheat> cheats = new HashSet<Cheat>();
        int maxX = this.course.MaxBy(kvp => kvp.Key.X).Key.X - 1;
        int maxY = this.course.MaxBy(kvp => kvp.Key.Y).Key.Y - 1;

        // Iterate over each point along the path, look for potential cheats, and add them to the collection.
        foreach (Point point in this.path)
        {
            int? distanceToPoint = this.dijkstraMap[point];

            if (!distanceToPoint.HasValue)
            {
                throw new Exception("Point along path has no distance on dijkstra map");
            }

            // Look for cheats.
            // Loop over all reachable points within cheat range and check if they're within range.
            int yStart = Math.Max(-this.maxCheatSteps, 1 - point.Y);
            int yEnd = Math.Min(this.maxCheatSteps, maxY - point.Y);
            for (int ySteps = yStart; ySteps <= yEnd; ySteps++)
            {
                // Calculate our x-bounds.
                int remainingSteps = this.maxCheatSteps - Math.Abs(ySteps);
                int xStart = Math.Max(-remainingSteps, 1 - point.X);
                int xEnd = Math.Min(remainingSteps, maxX - point.X);

                for (int xSteps = xStart; xSteps <= xEnd; xSteps++)
                {
                    // Determine our new point.
                    Point cheatPoint = new Point(point.X + xSteps, point.Y + ySteps);

                    // Check if the point is an open space.
                    int? distanceToCheatPoint = this.dijkstraMap[cheatPoint];
                    if (!distanceToCheatPoint.HasValue)
                    {
                        // This point must be a wall. Skip it.
                        continue;
                    }

                    // Check if the point is further along the path than we are.
                    int cheatSteps = Math.Abs(ySteps) + Math.Abs(xSteps);
                    int stepsSaved = distanceToCheatPoint.Value - (distanceToPoint.Value + cheatSteps);
                    if (stepsSaved > 0)
                    {
                        // This cheat will save us time. Add it to the collection.
                        cheats.Add(new Cheat(point, cheatPoint, stepsSaved));
                    }
                }
            }
        }

        return cheats;
    }
}
