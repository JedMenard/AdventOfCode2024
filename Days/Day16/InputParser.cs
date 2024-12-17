using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day16;

public class InputParser
{
    private ReindeerGame game;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day16\\input.txt");

        this.game = new ReindeerGame(Grid<char>.FromStreamReader(inputFile));
    }

    public int GetMinimumDistance()
    {
        return this.game.FindDistanceToEnd();
    }

    public int CountPointsAlongShortestPaths()
    {
        return this.game.CountPointsAlongShortestPaths();
    }
}
