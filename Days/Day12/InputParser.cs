using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day12;

public class InputParser
{
    Grid<char> grid;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day12\\input.txt");
        this.grid = Grid<char>.FromStreamReader(inputFile);
    }

    public int SumFenceCost()
    {
        // First, get all the contiguous regions in the grid.
        List<Grid<char>> regions = this.grid.GetContiguousRegions();

        return regions.Sum(region => region.Area * region.Perimeter);
    }

    public int SumBulkFenceCost()
    {
        // First, get all the contiguous regions in the grid.
        List<Grid<char>> regions = this.grid.GetContiguousRegions();

        return regions.Sum(region => region.Area * region.CountSides());
    }
}
