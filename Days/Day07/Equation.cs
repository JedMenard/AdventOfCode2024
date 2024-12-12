using AdventOfCode2024.Days.Day07.Operators;

namespace AdventOfCode2024.Days.Day07;

public class Equation
{
    public long Result;
    public List<long> Values;

    private static List<Operator> operators = new List<Operator>
    {
        new AdditionOperator(),
        new ProductOperator(),
        new ConcatOperator()
    };

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
        IEnumerable<long> possibleValues = operators.Select(op => op.Apply(first, second));

        // Base case.
        if (values.Count == 2)
        {
            return possibleValues.Any(val => val == this.Result);
        }

        // Recursively check if the equation is solveable.
        List<long> remainingValues = values.GetRange(2, values.Count - 2);
        return possibleValues.Any(val => this.isSolveable(remainingValues.Prepend(val).ToList()));
    }
}
