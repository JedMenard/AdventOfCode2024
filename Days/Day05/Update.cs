using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day05;

public class Update
{
    public List<Page> Pages;

    /// <summary>
    /// Gets the value of the page in the middle of the list.
    /// </summary>
    public int MiddlePageNumber => this.Pages[(this.Pages.Count - 1) / 2].Value;

    public Update(List<int> pages, Dictionary<int, List<int>> orderingRules)
    {
        this.Pages = new List<Page>();

        foreach (int page in pages)
        {
            this.Pages.Add(new Page(page, orderingRules.GetValueOrDefault(page)));
        }
    }

    /// <summary>
    /// Checks the rules of all pages in the update and determines if the overall update is valid.
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        // Loop over every page and check to make sure that
        // its associated rules are being followed.
        for (int i = 0; i < this.Pages.Count; i++)
        {
            Page page = this.Pages[i];

            // Check to see if there are any associated rules with this page number.
            if (page.Rules != null)
            {
                // There are rules, verify that none are being broken.
                IEnumerable<int> preceedingPageNumbers = this.Pages.GetRange(0, i).Select(p => p.Value);

                if (page.Rules.ContainsAny(preceedingPageNumbers)) {
                    // Rule broken, return false.
                    return false;
                }
            }
        }

        // No rules are being broken, so return true.
        return true;
    }
}
