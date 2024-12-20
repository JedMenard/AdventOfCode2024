using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day20;

public class Race
{
    private Grid<char> course;
    private Grid<int?> dijkstraMap;
    private List<Point> path;
    private Point startPoint;
    private Point endPoint;

    public Race(StreamReader input)
    {
        this.course = Grid<char>.FromStreamReader(input);

        this.startPoint = this.course.First(kvp => kvp.Value == 'S').Key;
        this.endPoint = this.course.First(kvp => kvp.Value == 'E').Key;

        this.dijkstraMap = this.course.BuildSinglePathDijkstraMap(this.startPoint, out this.path);
    }

    public List<Cheat> FindAllCheats()
    {
        List<Cheat> cheats = new List<Cheat>();

        // Iterate over each point along the path, look for potential cheats, and add them to the collection.
        foreach (Point point in this.path)
        {
            int? distanceToPoint = this.dijkstraMap[point];

            if (!distanceToPoint.HasValue)
            {
                throw new Exception("Point along path has no distance on dijkstra map");
            }

            // Look for cheats in each direction.
            foreach (DirectionEnum direction in DirectionEnumExtensions.CardinalDirections)
            {
                // For this to be a valid cheat, there has to be a wall in this direction.
                // Additionally, there has to be an open space on the other side of the wall.
                Point wallPoint = point.GetNextPointInDirection(direction, 1);
                Point cheatPoint = point.GetNextPointInDirection(direction, 2);

                if (this.course.PointIsValid(cheatPoint) && this.course[wallPoint] == '#' && this.course[cheatPoint] != '#')
                {
                    // There is a potential cheat here.
                    // Verify that it will move us forward along the path.
                    int? distanceToCheatPoint = this.dijkstraMap[cheatPoint];

                    if (!distanceToCheatPoint.HasValue)
                    {
                        throw new Exception("Cheat point has no distance on dijkstra map");
                    }

                    int stepsSaved = distanceToCheatPoint.Value - (distanceToPoint.Value + 2);
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
