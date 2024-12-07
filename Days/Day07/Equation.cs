namespace AdventOfCode2024.Days.Day07;

public class Equation
{
    public long Result;
    public List<long> Values;

    public Equation(long result, List<long> values)
    {
        this.Result = result;
        this.Values = values;
    }

    /// <summary>
    /// Whether or not there is a valid combination of operators that can solve this equation.
    /// </summary>
    /// <returns></returns>
    public bool IsSolveable()
    {
        return this.isSolveable(this.Values);
    }

    /// <summary>
    /// Recursively checks if the equation is solveable.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private bool isSolveable(List<long> values)
    {
        // This should never happen, but check for good measure.
        if (values.Count < 2)
        {
            throw new ApplicationException("Equation provided with too few values.");
        }

        // Cache some values.
        long first = values[0];
        long second = values[1];
        long additionValue = first + second;
        long productValue = first * second;
        long concatValue = long.Parse(first.ToString() + second.ToString());

        // Base case.
        if (values.Count == 2)
        {
            return additionValue == this.Result
                || productValue == this.Result
                || concatValue == this.Result;
        }

        // Recursively check if the equation is solveable.
        List<long> remainingValues = values.GetRange(2, values.Count - 2);
        return this.isSolveable(remainingValues.Prepend(additionValue).ToList())
            || this.isSolveable(remainingValues.Prepend(productValue).ToList())
            || this.isSolveable(remainingValues.Prepend(concatValue).ToList());
    }
}
