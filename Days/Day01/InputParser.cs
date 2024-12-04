namespace AdventOfCode2024.Days.Day01;

public class InputParser
{
    public List<int> Left;
    public List<int> Right;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day01\\input.txt");
        this.Left = new List<int>();
        this.Right = new List<int>();

        string? line = inputFile.ReadLine();
        while (line != null)
        {
            string[] inputValues = line.Split("   ");

            if (inputValues.Length != 2)
            {
                throw new ApplicationException($"Unexpected number of input values: {inputValues.Length}");
            }

            Left.Add(int.Parse(inputValues[0]));
            Right.Add(int.Parse(inputValues[1]));

            line = inputFile.ReadLine();
        }

        this.Left.Sort();
        this.Right.Sort();
    }

    public int GetDistance()
    {
        int distance = 0;

        for (int i = 0; i < this.Left.Count; i++)
        {
            distance += Math.Abs(this.Left[i] - this.Right[i]);
        }

        return distance;
    }

    public int GetSimilarityScore()
    {
        return this.Left.Sum(left => left * this.Right.Count(right => right == left));
    }
}
