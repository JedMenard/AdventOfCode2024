namespace AdventOfCode2024.Days.Day20;

public class InputParser
{
    private Race race;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day20\\input.txt");
        this.race = new Race(inputFile);
    }

    public int CountCheatsOfSize(int stepsSaved)
    {
        return this.race.FindAllCheats().Count(cheat => cheat.StepsSaved >= stepsSaved);
    }
}
