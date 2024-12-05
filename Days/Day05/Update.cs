using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day05;

public class Update
{
    public List<int> Pages;

    public int MiddlePageNumber => this.Pages[(this.Pages.Count - 1) / 2];

    public Update(List<int> pages)
    {
        this.Pages = pages;
    }

    public bool IsValid(Dictionary<int, List<int>> orderingRules)
    {
        // Loop over every page and check to make sure that
        // its associated rules are being followed.
        for (int i = 0; i < this.Pages.Count; i++)
        {
            int pageNumber = this.Pages[i];

            // Check to see if there are any associated rules with this page number.
            if (orderingRules.ContainsKey(pageNumber))
            {
                // There are rules, verify that none are being broken.
                if (orderingRules[pageNumber].ContainsAny(this.Pages.GetRange(0, i))) {
                    // Rule broken, return false.
                    return false;
                }
            }
        }

        // No rules are being broken, so return true.
        return true;
    }
}
