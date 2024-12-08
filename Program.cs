﻿using AdventOfCode2024.Days.Day09;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Console.WriteLine(new InputParser().DefragAndGetChecksum());

        sw.Stop();

        Console.WriteLine($"Processing time (ms): {sw.ElapsedMilliseconds}");
    }
}