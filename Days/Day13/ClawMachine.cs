using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AdventOfCode2024.Days.Day13;

public class ClawMachine
{
    private (long X, long Y) buttonA;
    private (long X, long Y) buttonB;
    private (long X, long Y) prize;

    private const int MAX_BUTTON_PRESSES = 100;

    public ClawMachine(List<string> input)
    {
        // Verify that we were given exactly three input lines,
        // one for button A, one for button B, and one for the prize.
        if (input.Count != 3)
        {
            throw new ArgumentException("Claw machine constructor requires exactly three lines of input.");
        }

        // Parse the three inputs out.
        string buttonAInput = input[0];
        string buttonBInput = input[1];
        string prizeInput = input[2];

        // Verify that they were all given in the right format.
        if (!buttonAInput.StartsWith("Button A:"))
        {
            throw new ArgumentException("Unexpected input for button A: " + buttonAInput);
        }

        if (!buttonBInput.StartsWith("Button B:"))
        {
            throw new ArgumentException("Unexpected input for button B: " + buttonBInput);
        }

        if (!prizeInput.StartsWith("Prize:"))
        {
            throw new ArgumentException("Unexpected input for prize: " + prizeInput);
        }

        // Remove the prelude from each.
        buttonAInput = buttonAInput.Substring(10);
        buttonBInput = buttonBInput.Substring(10);
        prizeInput = prizeInput.Substring(7);

        // Split on the X and Y delimeter.
        string[] buttonAValueStrings = buttonAInput.Split(", ");
        string[] buttonBValueStrings = buttonBInput.Split(", ");
        string[] prizeValueStrings = prizeInput.Split(", ");

        // Verify that we were given both an X and Y value for all three.
        if (buttonAValueStrings.Length != 2)
        {
            throw new ArgumentException("Unexpected values for button A: " + buttonAValueStrings);
        }

        if (buttonBValueStrings.Length != 2)
        {
            throw new ArgumentException("Unexpected values for button B: " + buttonBValueStrings);
        }

        if (prizeValueStrings.Length != 2)
        {
            throw new ArgumentException("Unexpected values for prize: " + prizeValueStrings);
        }

        // Parse the values into ints.
        List<int> buttonAValues = buttonAValueStrings.Select(val => int.Parse(val.Substring(2))).ToList();
        List<int> buttonBValues = buttonBValueStrings.Select(val => int.Parse(val.Substring(2))).ToList();
        List<int> prizeValues = prizeValueStrings.Select(val => int.Parse(val.Substring(2))).ToList();

        // Store the results.
        this.buttonA = (buttonAValues[0], buttonAValues[1]);
        this.buttonB = (buttonBValues[0], buttonBValues[1]);
        this.prize = (10000000000000 + prizeValues[0], 10000000000000 + prizeValues[1]);
    }

    public long GetMinCoinCount()
    {
        (long aPresses, long bPresses)? solution = this.FindSolution();

        return solution.HasValue
            ? solution.Value.aPresses * 3 + solution.Value.bPresses
            : 0;
    }

    private (long aPresses, long bPresses)? FindSolution()
    {
        // Set up our matrices.
        Matrix<double> A = DenseMatrix.OfArray(new double[,]
        {
            { buttonA.X, buttonB.X },
            { buttonA.Y, buttonB.Y }
        });

        Matrix<double> B = DenseMatrix.OfArray(new double[,]
        {
            { prize.X },
            { prize.Y }
        });

        // Solve the system.
        Matrix<double> solution = A.Solve(B);

        // Cast to integers and parse out.
        long aPresses = (long)Math.Round(solution[0, 0]);
        long bPresses = (long)Math.Round(solution[1, 0]);

        // Verify that the solution is still valid.
        // If not, then there is no integer solution to the equation.
        if (this.buttonA.X * aPresses + this.buttonB.X * bPresses != this.prize.X
            || this.buttonA.Y * aPresses + this.buttonB.Y * bPresses != this.prize.Y)
        {
            return null;
        }

        // Solution found.
        return (aPresses, bPresses);
    }
}
