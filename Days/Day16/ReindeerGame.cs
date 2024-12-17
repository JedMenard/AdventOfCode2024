using AdventOfCode2024.Shared;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day16;

public class ReindeerGame
{
    private Grid<char> map;
    private Grid<int?> distanceMap;
    private Point startPoint;
    private Point endPoint;

    public ReindeerGame(Grid<char> input)
    {
        this.map = input;
        this.distanceMap = new Grid<int?>();
        this.startPoint = new Point(0, 0);
        this.endPoint = new Point(0, 0);

        // Iterate over the map to build our distance map and set our start and end points.
        foreach ((Point point, char marker) in this.map)
        {
            if (marker == 'S')
            {
                this.startPoint = point;
                this.distanceMap[point] = 0;
            }
            else
            {
                this.distanceMap[point] = null;

                if (marker == 'E')
                {
                    this.endPoint = point;
                }
            }
        }
    }

    public int FindDistanceToEnd()
    {
        // Reindeer always start facing East.
        // Call the traversal method to build the distance map.
        this.Traverse(this.startPoint, DirectionEnum.East, 0);

        return this.distanceMap[this.endPoint].Value;
    }

    public int CountPointsAlongShortestPaths()
    {
        this.Traverse(this.startPoint, DirectionEnum.East, 0);

        HashSet<Point> points = new HashSet<Point> { this.startPoint };

        this.GetPointsInPathsOfMaxLength(
                    this.startPoint,
                    DirectionEnum.East,
                    0,
                    this.distanceMap[this.endPoint].Value,
                    points
                );

        return points.Count;
    }

    /// <summary>
    /// Traverses the maze, and populates the distance map.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="direction"></param>
    /// <param name="distanceToLocation"></param>
    private void Traverse(Point location, DirectionEnum direction, int distanceToLocation)
    {
        // If this is our first time visiting this point,
        // or if we've found a shorter path than previously,
        // then save the distance.
        if (!this.distanceMap.PointIsFilled(location) || this.distanceMap[location] > distanceToLocation)
        {
            this.distanceMap[location] = distanceToLocation;
        }

        // This method assumes forward traversal of the map.
        // Therefore, there are only three points we care about:
        // to the left, straight ahead, and to the right.
        // We're going to signify that as -1, 0, or 1 clockwise turns, respectively.
        for (int turns = -1; turns <= 1; turns++)
        {
            // Cache the new direction.
            // Notably, DirectionEnums include off-cardinal directions, like Northwest,
            // so we have to double the amount of turns.
            DirectionEnum newDirection = direction.TurnClockwise(turns * 2);

            // Determine where this next point is.
            Point nextPoint = location.GetNextPointInDirection(newDirection);

            // Verify that the next point isn't a wall.
            if (this.map[nextPoint] == '#')
            {
                // We've hit a wall, nothing left to do here.
                continue;
            }

            // Determine the distance for the next point.
            int nextStepPathLength = distanceToLocation + (1000 * Math.Abs(turns)) + 1;

            // If we've already traversed this point, find the distance.
            int? distanceAtNextLocation = this.distanceMap[nextPoint];

            // Check if we've visited this spot before.
            if (!distanceAtNextLocation.HasValue || nextStepPathLength < distanceAtNextLocation)
            {
                // This is either a new location,
                // or we've found a shorter path to an already-visited location.
                // Either way, update the minimum distance, and continue traversal.
                this.distanceMap[nextPoint] = nextStepPathLength;
                this.Traverse(nextPoint, newDirection, nextStepPathLength);
            }

            // We have visited this spot before from a shorter path.
            // However, since directionality matters,
            // there might still be points one step further that could be closer than we've seen before.
            // As such, if the difference in distances is less than 1000,
            // continue the traversal without updating the distance map.
            else if (nextStepPathLength - distanceAtNextLocation < 1000)
            {
                this.Traverse(nextPoint, newDirection, nextStepPathLength);
            }

            // At this point, we can tell it's not worth traversing to the new location from here.
        }
    }

    /// <summary>
    /// Recursively populates the validPoints parameter with all possible shortest path points.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="direction"></param>
    /// <param name="distanceToLocation"></param>
    /// <param name="maxDistance"></param>
    /// <param name="validPoints"></param>
    /// <returns></returns>
    private bool GetPointsInPathsOfMaxLength(Point location, DirectionEnum direction, int distanceToLocation, int maxDistance, HashSet<Point> validPoints)
    {
        // Base case: We've moved more steps than allowed.
        if (distanceToLocation > maxDistance)
        {
            return false;
        }

        // Another base case: We're at the destination.
        if (location == this.endPoint)
        {
            validPoints.Add(location);
            return true;
        }

        bool foundPath = false;

        // This method assumes forward traversal of the map.
        // Therefore, there are only three points we care about:
        // to the left, straight ahead, and to the right.
        // We're going to signify that as -1, 0, or 1 clockwise turns, respectively.
        for (int turns = -1; turns <= 1; turns++)
        {
            // Cache the new direction.
            // Notably, DirectionEnums include off-cardinal directions, like Northwest,
            // so we have to double the amount of turns.
            DirectionEnum newDirection = direction.TurnClockwise(turns * 2);

            // Determine where this next point is.
            Point nextPoint = location.GetNextPointInDirection(newDirection);

            // Verify that the next point is not a wall.
            if (this.map[nextPoint] == '#')
            {
                // We've hit a wall, nothing left to do here.
                continue;
            }

            // Determine the distance for the next point.
            int nextStepPathLength = distanceToLocation + (1000 * Math.Abs(turns)) + 1;

            // If we've already traversed this point, find the distance.
            int? distanceAtNextLocation = this.distanceMap[nextPoint];

            // Check if this is a potentially valid move.
            if (nextStepPathLength <= distanceAtNextLocation || nextStepPathLength - distanceAtNextLocation <= 1000)
            {
                // This could be a shortest path. Try it.
                if (this.GetPointsInPathsOfMaxLength(nextPoint, newDirection, nextStepPathLength, maxDistance, validPoints))
                {
                    validPoints.Add(nextPoint);
                    foundPath = true;
                }
            }
        }

        // If we found a path, return true so that the children of the caller will know this is a valid path.
        return foundPath;
    }
}
