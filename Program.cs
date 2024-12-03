using AdventOfCode2024.Days.DayThree;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Console.WriteLine(new InputParser().GetResult());

        sw.Stop();

        Console.WriteLine($"Processing time (ms): {sw.ElapsedMilliseconds}");
    }
}