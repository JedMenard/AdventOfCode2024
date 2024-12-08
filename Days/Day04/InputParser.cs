using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day04;

public class InputParser
{
    private Dictionary<Point, char> Grid;
    private int MaxXValue;
    private int MaxYValue;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day04\\input.txt");
        this.Grid = new Dictionary<Point, char>();

        int y = 0;
        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine())
        {   
            for (int x = 0; x < line.Length; x++)
            {
                this.Grid[new Point(x, y)] = line[x];
                this.MaxXValue = Math.Max(this.MaxXValue, x);
            }

            this.MaxYValue = y;
            y++;
        }
    }

    /// <summary>
    /// Finds the overall number of "MAS" crosswords in the grid.
    /// </summary>
    /// <returns></returns>
    public int FindWordCount()
    {
        int foundWords = 0;

        // Iterate over the whole grid, allowing for a one-space padding on each side.
        for (int x = 1; x < this.MaxXValue; x++)
        {
            for (int y = 1; y < this.MaxYValue; y++)
            {
                // Check each location for how many words it makes.
                foundWords += this.CheckForCrossword(new Point(x, y)) ? 1 : 0;
            }
        }

        return foundWords;
    }

    /// <summary>
    /// Checks the diagonal directions for words that start at the given location.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    private bool CheckForCrossword(Point location)
    {
        int foundWords = 0;

        // The middle of the word is always 'A', so if this letter is not 'A', then this isn't a crossword.
        if (this.Grid[location] != 'A')
        {
            return false;
        }

        // Check each possible diagonal direction for a word.
        foreach (DirectionEnum direction in DirectionEnumExtensions.DiagonalDirections)
        {
            foundWords += IsWordInDirection(location, direction) ? 1 : 0;
        }

        // For this to be a true crossword, we have to have found two distinct instances of "mas".
        return foundWords > 1;
    }

    /// <summary>
    /// Checks for the letters "MAS" in the given direction.
    /// Assumes the given location has an A, and is the middle of the cross.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private bool IsWordInDirection(Point location, DirectionEnum direction)
    {
        return this.Grid[location.GetNextPointInDirection(direction, -1)] == 'M'
                    && this.Grid[location.GetNextPointInDirection(direction, 1)] == 'S';
    }
}
