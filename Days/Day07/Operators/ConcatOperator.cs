namespace AdventOfCode2024.Days.Day07.Operators;

public class ConcatOperator : Operator
{
    public override long Apply(long first, long second)
        => long.Parse(first.ToString() + second.ToString());
}
