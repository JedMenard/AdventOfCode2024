using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day19;

public class Onsen
{
    private Dictionary<int, HashSet<string>> towelsByLength = new Dictionary<int, HashSet<string>>();
    private List<string> towels;
    
    public Onsen(string towelString)
    {
        this.towels = towelString.Split(", ").ToList();
        foreach (string towel in this.towels)
        {
            this.towelsByLength.AddToSetOrInsert(towel.Length, towel);
        }
    }

    public bool PatternIsPossibleOld(string pattern)
    {
        // Base case, we've reached the end of the pattern.
        if (pattern.IsNullOrEmpty())
        {
            return true;
        }

        // Otherwise, loop through all the available towel patterns
        // and check if any can be used.
        foreach (int towelLength in this.towelsByLength.Keys)
        {
            if (towelLength > pattern.Length)
            {
                // If we're checking towels that are longer than the remaining string,
                // then there are no possible solutions.
                break;
            }

            string startPattern = pattern.Substring(0, towelLength);

            if (this.towelsByLength[towelLength].Contains(startPattern))
            {
                // We can use this towel in the pattern.
                // Determine if we can complete the pattern with this towel.
                string newPattern = pattern.Substring(towelLength);
                if (this.PatternIsPossibleOld(newPattern))
                {
                    // This towel is part of a correct solution. Return true.
                    return true;
                }

                // This towel isn't part of a correct solution, but the rest might be.
            }
        }

        // We ran out of towel patterns and none made a solution.
        // This pattern cannot be completed.
        return false;
    }

    public bool PatternIsPossible(string pattern)
    {
        Stack<string> towels = new Stack<string>();
        Dictionary<int, HashSet<string>> failedTowels = new Dictionary<int, HashSet<string>>();
        int level = 0;

        while (!pattern.IsNullOrEmpty())
        {
            bool foundTowel = false;
            if (!failedTowels.ContainsKey(level))
            {
                failedTowels[level] = new HashSet<string>();
            }

            // Loop through all the available towel patterns
            // and check if any can be used.
            foreach (int towelLength in this.towelsByLength.Keys)
            {
                // Verify that we even have room for this length of towel.
                if (towelLength > pattern.Length)
                {
                    continue;
                }

                // Find the target pattern.
                string startPattern = pattern.Substring(0, towelLength);

                // Check if we have a towel of the desired pattern.
                if (this.towelsByLength[towelLength].Except(failedTowels[level]).Contains(startPattern))
                {
                    // We can use this towel in the pattern.
                    // Add it to the stack, remove it from the pattern,
                    // mark ourselves as having found a towel, and move on.
                    towels.Push(startPattern);
                    pattern = pattern.Substring(towelLength);
                    foundTowel = true;
                    break;
                }
            }

            if (!foundTowel)
            {
                // No towel was found at this level.
                // If we're back to the original level, then there is no solution.
                if (level == 0)
                {
                    return false;
                }

                // Otherwise, there might still be a soltuion.
                // Go back a step, add the towel back to the pattern, and try again.
                level -= 1;
                string towel = towels.Pop();
                failedTowels[level].Add(towel);
                pattern = towel + pattern;
            }
            else
            {
                // Found a towel, increment the level counter.
                level += 1;
            }
        }

        return true;
    }
}
