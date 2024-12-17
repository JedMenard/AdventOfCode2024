using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days.Day17;

public class Computer
{
    private int registerA;
    private int registerB;
    private int registerC;

    private int[] program;

    public Computer(int registerA, int registerB, int registerC, string program)
    {
        this.registerA = registerA;
        this.registerB = registerB;
        this.registerC = registerC;
        this.program = program.Split(",").Select(int.Parse).ToArray();
    }

    public string ExecuteProgram()
    {
        int instructionPointer = 0;
        List<int> output = new List<int>();

        // While looping, we need to guarantee that we've got room
        // for both the op code and the operand.
        while (instructionPointer <= this.program.Length - 2)
        {
            // We should also guarantee that we're at an even index.
            if (instructionPointer % 2 != 0)
            {
                throw new Exception("OpCode is no longer aligned.");
            }

            // Determine the op code and operand.
            int opCode = this.program[instructionPointer];
            int operand = this.program[instructionPointer + 1];

            // Execute the operation.
            int? result = this.ExecuteOpCode(opCode, operand);

            // If the operation is a print operation, add the result to our collection.
            if (opCode == 5 && result.HasValue)
            {
                output.Add(result.Value);
            }

            // Increment our loop variable.
            bool shouldIncrementPointer;
            if (opCode == 3)
            {
                // Successful jump operations shouldn't increment the counter.
                // Instead, move the pointer to the provided value.
                instructionPointer = result.HasValue ? result.Value : instructionPointer + 2;
            }
            else
            {
                // Increment the pointer by two.
                instructionPointer += 2;
            }
        }

        return output.Join(",");
    }

    private int? ExecuteOpCode(int opcode, int operand)
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
    private int GetComboOperand(int combo)
    {
        switch (combo)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return combo;
            case 4:
                return this.registerA;
            case 5:
                return this.registerB;
            case 6:
                return this.registerC;
            default:
                throw new ArgumentException("Only operands 0-6 are valid combo operands.");
        }
    }

    #region Operations

    /// <summary>
    /// Divides register A by the combo operand and stores the result in register A.
    /// </summary>
    /// <param name="combo"></param>
    private void adv(int combo)
    {
        int numerator = this.registerA;
        double denomenator = Math.Pow(2, this.GetComboOperand(combo));
        int result = (int)(numerator / denomenator);
        this.registerA = result;
    }

    /// <summary>
    /// Performs a bitwise XOR on register B with the provided literal
    /// and stores the result in register B.
    /// </summary>
    /// <param name="literal"></param>
    private void bxl(int literal)
    {
        this.registerB = this.registerB ^ literal;
    }

    /// <summary>
    /// Mods the combo operator's value by 8 and stores the result in register B.
    /// </summary>
    /// <param name="combo"></param>
    private void bst(int combo)
    {
        this.registerB = this.GetComboOperand(combo) % 8;
    }

    /// <summary>
    /// Jump operation.
    /// If a jump occurs, returns the value of the new index.
    /// If not, returns null.
    /// </summary>
    private int? jnz(int literal)
    {
        // Do nothing if register A has a zero-value.
        return this.registerA == 0
            ? null
            : literal;
    }

    /// <summary>
    /// Performs a bitwise XOR on registers B and C, then stores the result in register B.
    /// </summary>
    /// <param name="operand"></param>
    private void bxc(int operand)
    {
        this.registerB = this.registerB ^ this.registerC;
    }

    /// <summary>
    /// Mods the combo operator by 8 and returns it.
    /// </summary>
    /// <param name="combo"></param>
    /// <returns></returns>
    private int outOp(int combo)
    {
        return this.GetComboOperand(combo) % 8;
    }

    /// <summary>
    /// Divides register A by the combo operand and stores the result in register B.
    /// </summary>
    /// <param name="combo"></param>
    private void bdv(int combo)
    {
        int numerator = this.registerA;
        double denomenator = Math.Pow(2, this.GetComboOperand(combo));
        int result = (int)(numerator / denomenator);
        this.registerB = result;
    }

    /// <summary>
    /// Divides register A by the combo operand and stores the result in register C.
    /// </summary>
    /// <param name="combo"></param>
    private void cdv(int combo)
    {
        int numerator = this.registerA;
        double denomenator = Math.Pow(2, this.GetComboOperand(combo));
        int result = (int)(numerator / denomenator);
        this.registerC = result;
    }

    #endregion
}
