using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day15;

public class Warehouse
{
    public List<string> Original;
    public List<string> WideVersion;

    public Warehouse(StreamReader input)
    {
        this.Original = new List<string>();
        this.WideVersion = new List<string>();

        string? line = input.ReadLine();
        while (!line.IsNullOrEmpty())
        {
            this.Original.Add(line);
            this.WideVersion.Add(this.MakeWide(line));
            line = input.ReadLine();
        }
    }

    /// <summary>
    /// Follows the provided rules to make the input twice as wide.
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private string MakeWide(string original)
    {
        string wideVersion = "";

        foreach (char c in original)
        {
            switch (c)
            {
                case '#':
                    wideVersion += "##";
                    break;
                case 'O':
                    wideVersion += "[]";
                    break;
                case '.':
                    wideVersion += "..";
                    break;
                case '@':
                    wideVersion += "@.";
                    break;
                default:
                    throw new ArgumentException("Unexpected map marker: " + c);
            }
        }

        return wideVersion;
    }
}
