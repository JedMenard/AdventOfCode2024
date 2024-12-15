using AdventOfCode2024.Shared;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day15;

public class Robot
{
    private List<DirectionEnum> moves;
    private Point location;

    public Robot(List<string> inputs, Point startingPoint)
    {
        this.location = startingPoint;
        this.moves = inputs.SelectMany(line => line.ToCharArray())
            .Select(DirectionEnumExtensions.GetDirectionForCharacter)
            .ToList();
    }

    /// <summary>
    /// Iterates through the robot's programmed moves along the given grid.
    /// </summary>
    /// <param name="grid"></param>
    public void MoveToCompletion(Grid<char> grid)
    {
        //Debug.WriteLine(grid.ToString());
        foreach (DirectionEnum direction in this.moves)
        {
            this.Move(direction, grid);

            //Debug.WriteLine("Move: " + direction.GetCharacterForDirection());
            //Debug.WriteLine(grid.ToString());
        }
    }

    /// <summary>
    /// Moves the robot in the given direction.
    /// If possible and necessary, will also move any boxes in its way.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="grid"></param>
    private void Move(DirectionEnum direction, Grid<char> grid)
    {
        // First, start by checking if the space in the given direction is empty.
        Point nextLocation = this.location.GetNextPointInDirection(direction);
        char markerAtNextLocation = grid[nextLocation];
        
        if (markerAtNextLocation == '#')
        {
            // Hit a wall, nothing to do.
            return;
        }
        else if (markerAtNextLocation == '.')
        {
            // We're moving to an empty space. Update the robot's location and be done.
            this.SetNewLocation(nextLocation, grid);
            return;
        }

        // The next space is not empty. Find the first empty space.
        // Iterate over all points in the given direction
        // until we either find one that's empty,
        // or run out of space on the grid.
        int steps = 2;
        Point targetPoint = this.location.GetNextPointInDirection(direction, steps);
        while(grid.PointIsValid(targetPoint))
        {
            char markerAtPoint = grid[targetPoint];

            // Check if we've found a wall.
            if (markerAtPoint == '#')
            {
                // Hit a wall, nowhere left to move.
                return;
            }

            // Check if this space is empty.
            if (markerAtPoint == '.')
            {
                // The space is empty!
                // Simplify the shift by moving the markerAtNextLocation to the targetPoint.
                grid[targetPoint] = grid[nextLocation];
                this.SetNewLocation(nextLocation, grid);
                return;
            }

            // Must be another box. Keep looking.
            targetPoint = this.location.GetNextPointInDirection(direction, ++steps);
        }
    }

    /// <summary>
    /// Sets the robot's position in the grid to the new position.
    /// </summary>
    /// <param name="newLocation"></param>
    /// <param name="grid"></param>
    private void SetNewLocation(Point newLocation, Grid<char> grid)
    {
        // First, set the old location to an empty space.
        grid[this.location] = '.';

        // Now set the new location to the robot marker.
        grid[newLocation] = '@';

        // Lastly, update the location on the robot.
        this.location = newLocation;
    }
}
