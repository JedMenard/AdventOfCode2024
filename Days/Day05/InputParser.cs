using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day05;

public class InputParser
{
    private List<Update> updates;
    private Dictionary<int, List<int>> orderingRules;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day05\\input.txt");
        this.updates = new List<Update>();
        this.orderingRules = new Dictionary<int, List<int>>();

        bool processingRules = true;
        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine())
        {
            // The rules and updates are separated by an empty line.
            if (line.IsNullOrEmpty())
            {
                processingRules = false;
                continue;
            }

            if (processingRules)
            {
                // The rules are in the format ##|##, where the first number is the key
                // and the second number is a value that must come after the key.
                // First, split the line and cast them to ints.
                int[] ruleValues = line.Split("|").Select(int.Parse).ToArray();

                // There should only ever be exactly two values on a line.
                if (ruleValues.Length != 2)
                {
                    throw new ArgumentException($"Unexpected input: {line}");
                }

                // Store the rule.
                this.orderingRules.AddToListOrInsert(ruleValues[0], ruleValues[1]);
            }
            else
            {
                // The updates are in the format ##,##,##...
                // First, parse the string into a list of ints.
                List<int> updateValues = line.Split(",").Select(int.Parse).ToList();

                // Add this update to the list.
                this.updates.Add(new Update(updateValues, this.orderingRules));
            }
        }
    }

    public int SumValidPageNumbers()
    {
        return this.updates.Where(u => u.IsValid())
            .Sum(u => u.MiddlePageNumber);
    }

    public int SortAndSumInvalidUpdates()
    {
        List<Update> invalidUpdates = this.updates.Where(u => !u.IsValid()).ToList();
        foreach (Update update in invalidUpdates)
        {
            // Page objects have a custom-defined comparator,
            // so we can just call the .Sort() method here.
            update.Pages.Sort();
        }

        return invalidUpdates.Sum(u => u.MiddlePageNumber);
    }
}
