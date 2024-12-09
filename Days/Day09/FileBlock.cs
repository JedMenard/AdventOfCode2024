namespace AdventOfCode2024.Days.Day09;

public class FileBlock
{
    public int? FileId;
    public int Size;

    public FileBlock(int? value, int size)
    {
        this.FileId = value;
        this.Size = size;
    }

    public bool IsEmpty => !this.FileId.HasValue;

    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < this.Size; i++)
        {
            str += this.FileId?.ToString() ?? ".";
        }
        return str;
    }
}
