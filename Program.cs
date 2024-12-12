using AdventOfCode2024.Days.Day12;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Console.WriteLine(new InputParser().SumFenceCost());

        sw.Stop();

        Console.WriteLine($"Processing time (ms): {sw.ElapsedMilliseconds}");
    }
}