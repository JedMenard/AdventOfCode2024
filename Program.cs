using AdventOfCode2024.Days.Day18;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        string result = new InputParser().FindFirstBlockingWall().ToString() ?? "No valid path.";

        sw.Stop();

        Console.WriteLine(result);
        Debug.WriteLine(result);
        Console.WriteLine($"Processing time (ms): {sw.ElapsedMilliseconds}");
    }
}