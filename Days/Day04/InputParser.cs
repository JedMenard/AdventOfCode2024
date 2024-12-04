using AdventOfCode2024.Days.Day02;

namespace AdventOfCode2024.Days.Day04;

public class InputParser
{
    private Dictionary<Location, char> Grid;
    private int MaxXValue;
    private int MaxYValue;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day04\\input.txt");
        this.Grid = new Dictionary<Location, char>();

        int y = 0;
        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine())
        {   
            for (int x = 0; x < line.Length; x++)
            {
                this.Grid[new Location(x, y)] = line[x];
                this.MaxXValue = Math.Max(this.MaxXValue, x);
            }

            this.MaxYValue = y;
            y++;
        }
    }

    /// <summary>
    /// Finds the overall number of "XMAS" words in the grid.
    /// </summary>
    /// <returns></returns>
    public int FindWordCount()
    {
        int foundWords = 0;

        // Iterate over the whole grid.
        for (int x = 0; x <= this.MaxXValue; x++)
        {
            for (int y = 0; y <= this.MaxYValue; y++)
            {
                // Check each location for how many words it makes.
                foundWords += this.CheckAllDirectionsForWord(new Location(x, y));
            }
        }

        return foundWords;
    }

    /// <summary>
    /// Checks all cardinal directions for words that start at the given location.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    private int CheckAllDirectionsForWord(Location location)
    {
        int foundWords = 0;

        // The word starts with X, so if this character is not X, then there are no words here.
        if (this.Grid[location] != 'X')
        {
            return foundWords;
        }

        // Check each possible direction for a word.
        foreach (DirectionEnum direction in Enum.GetValues(typeof(DirectionEnum)))
        {
            foundWords += IsWordInDirection(location, direction) ? 1 : 0;
        }

        return foundWords;
    }

    /// <summary>
    /// Checks for the letters "MAS" in the given direction.
    /// Assumes the given location has an X.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private bool IsWordInDirection(Location location, DirectionEnum direction)
    {
        if (!this.CanGoInDirection(location, direction))
        {
            return false;
        }

        return this.Grid[location.GetLocationInDirection(direction, 1)] == 'M'
                    && this.Grid[location.GetLocationInDirection(direction, 2)] == 'A'
                    && this.Grid[location.GetLocationInDirection(direction, 3)] == 'S';
    }

    /// <summary>
    /// Checks if the location three steps away in the given direction from the given point is valid.
    /// Valid means between zero and the max value, inclusive.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private bool CanGoInDirection(Location location, DirectionEnum direction)
    {
        Location newLocation = location.GetLocationInDirection(direction, 3);
        return (0 <= newLocation.X && newLocation.X <= this.MaxXValue)
            && (0 <= newLocation.Y && newLocation.Y <= this.MaxYValue);
    }
}
