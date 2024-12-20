using AdventOfCode2024.Shared;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day19;

public class Onsen
{
    private List<string> towels;
    
    public Onsen(string towelString)
    {
        this.towels = towelString.Split(", ").ToList();
    }

    public HashSet<string> GetAllSolutions(string pattern)
    {
        Stack<string> towelSolution = new Stack<string>();
        HashSet<string> solutions = new HashSet<string>();
        int level = 0;

        // Instantiate our processed towel list.
        List<HashSet<string>> processedTowels = new List<HashSet<string>>();
        for (int i = 0; i < pattern.Length; i++)
        {
            processedTowels.Add(new HashSet<string>());
        }

        while (!pattern.IsNullOrEmpty())
        {
            bool foundTowel = false;
            List<string> processedTowelsForLevel = processedTowels[level].ToList();

            // Loop through all the available towel patterns
            // and check if any can be used.
            for (int towelLength = 1; towelLength <= 8; towelLength++)
            {
                // Verify that we even have room for this length of towel.
                if (towelLength > pattern.Length)
                {
                    // No room, no point checking the longer lengths.
                    break;
                }

                // Find the target pattern.
                string startPattern = pattern.Substring(0, towelLength);

                // Check the potentials for an exact match.
                Stopwatch sw = new Stopwatch();
                sw.Start();
                bool v = this.towels.Contains(startPattern);
                sw.Stop();


                if (v && !processedTowelsForLevel.Contains(startPattern))
                {
                    // We can use this towel in the pattern.
                    // Add it to the stack,
                    // add it to the set of processed towels,
                    // remove it from the pattern,
                    // mark ourselves as having found a towel, and move on.
                    towelSolution.Push(startPattern);
                    processedTowelsForLevel.Add(startPattern);
                    pattern = pattern.Substring(towelLength);
                    foundTowel = true;
                    break;
                }
            }

            // Check if we've completed the pattern.
            if (pattern.IsNullOrEmpty())
            {
                // Pattern complete!
                // Convert the stack to a comma-separated string,
                // add it to the solution set,
                // clear the processed list for the level above,
                // take this towel off the solution stack,
                // and continue to check if there are any other solutions.
                solutions.Add(towelSolution.Reverse().Join(","));
                if (processedTowels.Count < level)
                {
                    processedTowels[level + 1] = [];
                }

                pattern = towelSolution.Pop();
                continue;
            }

            if (!foundTowel)
            {
                // No towel was found at this level.
                // If we're back to the original level, then we're done.
                if (level == 0)
                {
                    break;
                }

                // Otherwise, there might still be more soltuions.
                // All the towels that we processed for this level are no longer relevant,
                // so reset the list, go back a step, and try again.
                processedTowels[level] = [];
                level -= 1;
                string towel = towelSolution.Pop();
                pattern = towel + pattern;
            }
            else
            {
                // Found a towel, increment the level counter.
                level += 1;
            }
        }

        return solutions;
    }
}
