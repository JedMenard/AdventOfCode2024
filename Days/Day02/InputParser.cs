﻿namespace AdventOfCode2024.Days.Day02;

public class InputParser
{
    private List<Report> reports;

    public int ValidReportCount => this.reports.Count(report => report.IsSafe());

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day02\\input.txt");
        this.reports = new List<Report>();

        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine())
        {
            Report report = new Report(line);
            this.reports.Add(report);
        }
    }
}
