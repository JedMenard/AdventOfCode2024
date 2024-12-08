using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day08;

public class InputParser
{
    private Grid<char> grid;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day08\\input.txt");
        this.grid = Grid<char>.FromStreamReader(inputFile);
    }

    public int CountAntiNodes()
    {
        // First, find all non-"." nodes, grouped by character.
        Dictionary<char, List<Point>> allNodes = this.GetNodes();

        // Within each group, determine all possible anti-nodes, and union the results.
        HashSet<Point> antiNodes = new HashSet<Point>();
        foreach ((char marker, List<Point> nodesOfType) in allNodes)
        {
            antiNodes = antiNodes.Union(this.FindAntiNodes(nodesOfType)).ToHashSet();
        }

        // Count the results and return.
        return antiNodes.Count;
    }

    /// <summary>
    /// Gets all non-"." nodes, grouped by node character.
    /// </summary>
    /// <returns></returns>
    private Dictionary<char, List<Point>> GetNodes()
    {
        Dictionary<char, List<Point>> nodes = new Dictionary<char, List<Point>>();

        // Iterate over all elements in the grid.
        foreach ((Point point, char marker) in this.grid.AsEnumerable)
        {
            if (marker != '.')
            {
                // If it's a valid node marker, then add it to the collection.
                nodes.AddToListOrInsert(marker, point);
            }
        }

        return nodes;
    }

    /// <summary>
    /// Finds all possible anti-nodes within a group.
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    private HashSet<Point> FindAntiNodes(List<Point> nodes)
    {
        HashSet<Point> antiNodes = new HashSet<Point>();

        // Iterate over all possible node pairs.
        for (int i = 0; i < nodes.Count; i++)
        {
            // Cache the first node.
            Point firstNode = nodes[i];

            // Iterate over all remaining nodes.
            for (int j = i + 1; j < nodes.Count; j++)
            {
                // Cache the second node.
                Point secondNode = nodes[j];

                // Find the X and Y distances between nodes.
                int xDifference = firstNode.X - secondNode.X;
                int yDifference = firstNode.Y - secondNode.Y;

                // Find where the antinodes should be.
                Point firstAntiNode = new Point(firstNode.X + xDifference, firstNode.Y + yDifference);
                Point secondAntiNode = new Point(secondNode.X - xDifference, secondNode.Y - yDifference);

                // If the anti-nodes are valid in the grid, add them to the set.
                if (this.grid.PointIsValid(firstAntiNode))
                {
                    antiNodes.Add(firstAntiNode);
                }

                if (this.grid.PointIsValid(secondAntiNode))
                {
                    antiNodes.Add(secondAntiNode);
                }
            }
        }

        return antiNodes;
    }
}
