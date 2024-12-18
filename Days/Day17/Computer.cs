using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day17;

public class Computer
{
    public long RegisterA;
    public long RegisterB;
    public long RegisterC;

    public long[] Program;

    public Computer(long registerA, long registerB, long registerC, string program)
    {
        this.RegisterA = registerA;
        this.RegisterB = registerB;
        this.RegisterC = registerC;
        this.Program = program.Split(",").Select(long.Parse).ToArray();
    }

    public string ExecuteProgram()
    {
        long instructionPointer = 0;
        List<long> output = new List<long>();

        // While looping, we need to guarantee that we've got room
        // for both the op code and the operand.
        while (instructionPointer <= this.Program.Length - 2)
        {
            // We should also guarantee that we're at an even index.
            if (instructionPointer % 2 != 0)
            {
                throw new Exception("OpCode is no longer aligned.");
            }

            // Determine the op code and operand.
            long opCode = this.Program[instructionPointer];
            long operand = this.Program[instructionPointer + 1];

            // Execute the operation.
            long? result = this.ExecuteOpCode(opCode, operand);

            // If the operation is a prlong operation, add the result to our collection.
            if (opCode == 5 && result.HasValue)
            {
                output.Add(result.Value);
            }

            // Increment our loop variable.
            bool shouldIncrementPolonger;
            if (opCode == 3)
            {
                // Successful jump operations shouldn't increment the counter.
                // Instead, move the polonger to the provided value.
                instructionPointer = result.HasValue ? result.Value : instructionPointer + 2;
            }
            else
            {
                // Increment the polonger by two.
                instructionPointer += 2;
            }
        }

        return output.Join(",");
    }

    private long? ExecuteOpCode(long opcode, long operand)
    {
        switch (opcode){
            case 0:
                this.adv(operand);
                break;
            case 1:
                this.bxl(operand);
                break;
            case 2:
                this.bst(operand);
                break;
            case 3:
                return this.jnz(operand);
            case 4:
                this.bxc(operand);
                break;
            case 5:
                return this.outOp(operand);
            case 6:
                this.bdv(operand);
                break;
            case 7:
                this.cdv(operand);
                break;
            default:
                    throw new ArgumentException("Only op codes 0-7 are valid combo operands.");
            }

        // Any non-returning operations should return null.
        return null;
        }

    /// <summary>
    /// Fetches the appropriate value for the given combo operand.
    /// </summary>
    /// <param name="combo"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private long GetComboOperand(long combo)
    {
        switch (combo)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return combo;
            case 4:
                return this.RegisterA;
            case 5:
                return this.RegisterB;
            case 6:
                return this.RegisterC;
            default:
                throw new ArgumentException("Only operands 0-6 are valid combo operands.");
        }
    }

    #region Operations

    /// <summary>
    /// Divides register A by the combo operand and stores the result in register A.
    /// </summary>
    /// <param name="combo"></param>
    private void adv(long combo)
    {
        long numerator = this.RegisterA;
        double denomenator = Math.Pow(2, this.GetComboOperand(combo));
        long result = (long)(numerator / denomenator);
        this.RegisterA = result;
    }

    /// <summary>
    /// Performs a bitwise XOR on register B with the provided literal
    /// and stores the result in register B.
    /// </summary>
    /// <param name="literal"></param>
    private void bxl(long literal)
    {
        this.RegisterB = this.RegisterB ^ literal;
    }

    /// <summary>
    /// Mods the combo operator's value by 8 and stores the result in register B.
    /// </summary>
    /// <param name="combo"></param>
    private void bst(long combo)
    {
        this.RegisterB = this.GetComboOperand(combo) % 8;
    }

    /// <summary>
    /// Jump operation.
    /// If a jump occurs, returns the value of the new index.
    /// If not, returns null.
    /// </summary>
    private long? jnz(long literal)
    {
        // Do nothing if register A has a zero-value.
        return this.RegisterA == 0
            ? null
            : literal;
    }

    /// <summary>
    /// Performs a bitwise XOR on registers B and C, then stores the result in register B.
    /// </summary>
    /// <param name="operand"></param>
    private void bxc(long operand)
    {
        this.RegisterB = this.RegisterB ^ this.RegisterC;
    }

    /// <summary>
    /// Mods the combo operator by 8 and returns it.
    /// </summary>
    /// <param name="combo"></param>
    /// <returns></returns>
    private long outOp(long combo)
    {
        return this.GetComboOperand(combo) % 8;
    }

    /// <summary>
    /// Divides register A by the combo operand and stores the result in register B.
    /// </summary>
    /// <param name="combo"></param>
    private void bdv(long combo)
    {
        long numerator = this.RegisterA;
        double denomenator = Math.Pow(2, this.GetComboOperand(combo));
        long result = (long)(numerator / denomenator);
        this.RegisterB = result;
    }

    /// <summary>
    /// Divides register A by the combo operand and stores the result in register C.
    /// </summary>
    /// <param name="combo"></param>
    private void cdv(long combo)
    {
        long numerator = this.RegisterA;
        double denomenator = Math.Pow(2, this.GetComboOperand(combo));
        long result = (long)(numerator / denomenator);
        this.RegisterC = result;
    }

    #endregion
}
