namespace AdventOfCode2024.Days.DayTwo;

public class InputParser
{
    private List<Report> reports;

    public int ValidReportCount => this.reports.Count(report => report.IsSafe());

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\DayTwo\\input.txt");
        this.reports = new List<Report>();

        string? line = inputFile.ReadLine();
        while (line != null)
        {
            Report report = new Report(line);
            this.reports.Add(report);
            line = inputFile.ReadLine();
        }
    }
}
