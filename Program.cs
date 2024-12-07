using AdventOfCode2024.Days.Day07;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Console.WriteLine(new InputParser().SumValidEquations());

        sw.Stop();

        Console.WriteLine($"Processing time (ms): {sw.ElapsedMilliseconds}");
    }
}