using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day06;

public class LocationMarker
{
    public char Marker;
    private HashSet<DirectionEnum> traversedDirections;

    public LocationMarker(char marker)
    {
        this.Marker = marker;
        this.traversedDirections = new HashSet<DirectionEnum>();
    }

    public LocationMarker(LocationMarker other)
    {
        this.Marker = other.Marker;
        this.traversedDirections = new HashSet<DirectionEnum>(other.traversedDirections);
    }

    /// <summary>
    /// Stores the traversal direction on the marker.
    /// </summary>
    /// <param name="direction"></param>
    public void MarkTraversed(DirectionEnum direction)
    {
        this.traversedDirections.Add(direction);
    }

    /// <summary>
    /// Checks if this marker has been traversed at all.
    /// </summary>
    /// <returns></returns>
    public bool HasBeenTraversed()
    {
        return !this.traversedDirections.IsNullOrEmpty();
    }

    /// <summary>
    /// Checks if this marker has been traversed in the provided direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool HasBeenTraversedInDirection(DirectionEnum direction)
    {
        return this.traversedDirections.Contains(direction);
    }

    public void ResetTraversal()
    {
        this.traversedDirections = new HashSet<DirectionEnum>();
    }
}
