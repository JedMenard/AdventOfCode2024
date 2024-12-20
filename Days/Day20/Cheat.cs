using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day20;

public class Cheat
{
    public Point StartPoint;
    public Point EndPoint;
    public int StepsSaved;

    public Cheat(Point startPoint, Point endPoint, int stepsSaved)
    {
        this.StartPoint = startPoint;
        this.EndPoint = endPoint;
        this.StepsSaved = stepsSaved;
    }

    // Comparator override for dictionary hashing.
    public override int GetHashCode()
    {
        return HashCode.Combine(this.StartPoint, this.EndPoint);
    }
}
