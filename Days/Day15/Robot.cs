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
        foreach (DirectionEnum direction in this.moves)
        {
            this.Move(direction, grid);
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
        if (!this.CanShiftInDirection(this.location, direction, grid))
        {
            // We're attempting to move in a direction for which there is not enough space.
            return;
        }

        // This direction is valid for shifting, so perform the shift.
        this.Shift(this.location, direction, grid);

        // Lastly, update the robot's position.
        this.SetNewLocation(this.location.GetNextPointInDirection(direction), grid);
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

    /// <summary>
    /// Shifts all objects in the given direction until an open space is found.
    /// </summary>
    /// <param name="startLocation"></param>
    /// <param name="direction"></param>
    /// <param name="grid"></param>
    /// <exception cref="Exception"></exception>
    private void Shift(Point startLocation, DirectionEnum direction, Grid<char> grid)
    {
        // First, figure out where the first open space is.
        int steps = 1;
        Point firstOpenSpace = startLocation.GetNextPointInDirection(direction, steps);
        bool foundOpenSpace = false;
        while (grid.PointIsValid(firstOpenSpace))
        {
            // Verify that we're not hitting a wall.
            char marker = grid[firstOpenSpace];
            if (marker == '#')
            {
                // The caller should have verified already that there's enough room to shift.
                // Throw an error.
                throw new Exception("Ran out of space to perform shift.");
            }

            // If the space is empty, then our search is complete.
            if (marker == '.')
            {
                foundOpenSpace = true;
                break;
            }

            // Otherwise, there's a moveable obstacle in our way.
            // If we're moving a connected obstacle vertically,
            // then we need to shift its counterpart too.
            if (direction.IsVertical() && (marker == '[' || marker == ']'))
            {
                // Find the connected space.
                Point connectedSpace = marker == '['
                    ? firstOpenSpace.GetNextPointInDirection(DirectionEnum.East)
                    : firstOpenSpace.GetNextPointInDirection(DirectionEnum.West);

                // Shift it.
                this.Shift(connectedSpace, direction, grid);
            }

            // At this point, we don't care what's in our way,
            // we just need to look past it to find an empty space.
            steps++;
            firstOpenSpace = startLocation.GetNextPointInDirection(direction, steps);
        }

        // Verify that we found an open space.
        if (!foundOpenSpace)
        {
            throw new Exception("Unable to find an open space to perform shift.");
        }

        // Move backwards from the destination towards the source
        // and shift everything one at a time.
        for (int i = steps; i > 0; i--)
        {
            // Find the next point to shift.
            Point targetLocation = startLocation.GetNextPointInDirection(direction, i);
            Point sourceLocation = startLocation.GetNextPointInDirection(direction, i - 1);

            // Shift it.
            // Notably, we don't care what value is going to be at the sourceLocation for now,
            // since we know we're just going to overwrite it anyways.
            grid[targetLocation] = grid[sourceLocation];
        }

        // Finally, update the start location to an empty space.
        grid[startLocation] = '.';
    }

    /// <summary>
    /// Recursively checks if the given point is able to shift in the given direction.
    /// </summary>
    /// <param name="startLocation"></param>
    /// <param name="direction"></param>
    /// <param name="grid"></param>
    /// <param name="checkConnectedSpaces">Whether or not we should check connected spaces. Should only be false during recursion.</param>
    /// <returns></returns>
    private bool CanShiftInDirection(Point startLocation, DirectionEnum direction, Grid<char> grid, bool checkConnectedSpaces = true)
    {
        // If the location is outside of the grid,
        // or if the given location is occupied by a wall,
        // then we cannot shift in this direction.
        if (!grid.PointIsValid(startLocation))
        {
            return false;
        }

        char marker = grid[startLocation];
        if (marker == '#')
        {
            return false;
        }

        // If the location is empty, then we can shift in this direction.
        if (marker == '.')
        {
            return true;
        }

        // Otherwise, there's a moveable obstacle here.
        // Continue in the given direction, accounting for connected obstacles.
        // Obstacles are only connected horizontally,
        // so we can skip that check for horizontal shifts.
        Point nextLocation = startLocation.GetNextPointInDirection(direction);
        if (checkConnectedSpaces && direction.IsVertical() && (marker == '[' || marker == ']'))
        {
            // We're moving vertically, but moving a connected obstacle.
            // We have to continue checking in the current space,
            // but we also have to check the connected space.
            Point connectedSpace = marker == '['
                ? startLocation.GetNextPointInDirection(DirectionEnum.East)
                : startLocation.GetNextPointInDirection(DirectionEnum.West);

            // To shift, both spaces must be able to shift.
            return this.CanShiftInDirection(nextLocation, direction, grid)
                && this.CanShiftInDirection(connectedSpace, direction, grid, false);
        }

        // We are either shifting horizontally,
        // or we're shifting a single-space obstacle vertically.
        // Either way, all we have to do is continue checking in the given direction.
        return this.CanShiftInDirection(nextLocation, direction, grid);
    }
}
