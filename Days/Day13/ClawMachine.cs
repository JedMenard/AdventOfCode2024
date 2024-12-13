namespace AdventOfCode2024.Days.Day13;

public class ClawMachine
{
    private (int X, int Y) buttonA;
    private (int X, int Y) buttonB;
    private (int X, int Y) prize;

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
        this.prize = (prizeValues[0], prizeValues[1]);
    }

    public int GetMinCoinCount()
    {
        (int aPresses, int bPresses)? solution = this.FindSolution();

        return solution.HasValue
            ? solution.Value.aPresses * 3 + solution.Value.bPresses
            : 0;
    }

    private (int aPresses, int bPresses)? FindSolution()
    {
        // First, start with the upper bound for how many times we might have to press the B button.
        int maxBPresses = Math.Min(this.prize.X / this.buttonB.X, this.prize.Y / this.buttonB.Y);
        maxBPresses = Math.Min(maxBPresses, MAX_BUTTON_PRESSES);

        // Iterate backwards to zero to find the minimum number of times we can press the A button.
        for (; maxBPresses >= 0; maxBPresses--)
        {
            int remainingX = this.prize.X - (this.buttonB.X * maxBPresses);
            int remainingY = this.prize.Y - (this.buttonB.Y * maxBPresses);

            if (remainingX % this.buttonA.X != 0 || remainingY % this.buttonA.Y != 0)
            {
                // No amount of A presses will get us to the prize, so skip this loop.
                continue;
            }

            // Verify that both the X and Y values will be satisfied in the same number of presses.
            int aPressesForX = remainingX / this.buttonA.X;
            int aPressesForY = remainingY / this.buttonA.Y;
            if (aPressesForX == aPressesForY)
            {
                return (aPressesForX, maxBPresses);
            }
        }

        return null;
    }
}
