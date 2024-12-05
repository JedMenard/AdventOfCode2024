using AdventOfCode2024.Days.Day04;
using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day05;

public class Page : IComparable
{
    public int Value;
    public List<int>? Rules;

    public Page(int value, List<int>? rules)
    {
        this.Value = value;
        this.Rules = rules;
    }

    public int CompareTo(object? obj)
    {
        Page? other = (Page?)obj;
        if (other == null)
        {
            // Arbitrarily decide that null values go at the end.
            return this == null ? 0 : -1;
        }

        // The actual values of pages don't matter, beyond whether they're the same.
        // Instead, we use the rules list to determine ordering.
        // First, though, let's start with the "equal" case.
        if (this.Value == other.Value)
        {
            return 0;
        }

        // The rules list dictates which values are not allowed to preceed this page.
        // If the rules list contains the other value, then this value must come first.
        if (this.Rules != null && Rules.Contains(other.Value))
        {
            return -1;
        }

        // If the other page's rule list contains this value, then the other page must come first.
        if (other.Rules != null && other.Rules.Contains(this.Value))
        {
            return 1;
        }

        // If the pages have different values,
        // and neither page's rules mention the other value,
        // then we don't care about the order.
        return 0;
    }

    // Comparator override for dictionary hashing.
    public override bool Equals(object? obj)
        => obj is Page other && Value == other.Value;

    // Comparator override for dictionary hashing.
    public override int GetHashCode()
    {
        return (this.Value).GetHashCode();
    }
}
