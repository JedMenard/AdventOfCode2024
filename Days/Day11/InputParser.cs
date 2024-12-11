using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day11;

public class InputParser
{
    private List<Rock> rocks;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day11\\input.txt");

        this.rocks = inputFile.ReadLine()
            ?.Split(" ")
            .Select(long.Parse)
            .Select(val => new Rock(val))
            .ToList() ?? new List<Rock>();
    }

    public int CountRocksAfterBlinks(int blinks)
    {
        for (int i = 0; i < blinks; i++)
        {
            // Blink all the rocks.
            this.rocks = this.rocks.SelectMany(rock => rock.Blink()).ToList();
        }

        return this.rocks.Count;
    }
}
