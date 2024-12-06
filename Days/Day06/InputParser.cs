using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day06;

public class InputParser
{
    private Dictionary<Location, char> grid;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day06\\input.txt");
        this.grid = new Dictionary<Location, char>();

        int y = 0;
        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine(), y++)
        {
            for (int x = 0; x < line.Length; x++)
            {
                char v = line[x];
                this.grid[new Location(x, y)] = v;
            }
        }
    }

    public int CountTraversedLocations()
    {
        this.Traverse();

        return this.grid.Count(kvp => kvp.Value == 'X');
    }

    private void Traverse()
    {
        // First, figure out where the guard is and which direction she's facing.
        (Location location, char directionCharacter) = this.grid.First(kvp => "^>V<".Contains(kvp.Value));
        DirectionEnum direction = DirectionEnumExtensions.GetDirectionForCharacter(directionCharacter);

        // Now figure out where the guard is going next.
        Location nextStep = location.GetLocationInDirection(direction, 1);

        // Start patrolling.
        while (this.grid.ContainsKey(nextStep))
        {
            // Check if the next space forward is blocked.
            while (this.grid[nextStep] == '#')
            {
                // Next space is blocked, turn clockwise until we find a free space.
                direction = direction.TurnClockwise();
                nextStep = location.GetLocationInDirection(direction, 1);
            }

            // We are now facing an open direction.
            // Move forward one space and mark the location as passed.
            this.grid[location] = 'X';
            this.grid[nextStep] = direction.GetCharacterForDirection();
            location = nextStep;
            nextStep = location.GetLocationInDirection(direction, 1);
        }

        // The guard is now leaving the map. Change the character for her current position to an X.
        this.grid[location] = 'X';
    }
}
