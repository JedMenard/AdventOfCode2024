using AdventOfCode2024.Shared;
using System.Text;

namespace AdventOfCode2024.Days.Day09;

public class InputParser
{
    private List<FileBlock> blockList;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day09\\input.txt");
        this.blockList = new List<FileBlock>();

        bool isFile = true;
        int fileIdCounter = 0;
        for (string? line = inputFile.ReadLine(); line != null; line = inputFile.ReadLine())
        {
            foreach (char character in line)
            {
                // Determine which character(s) to fill into the file.
                int? fileId;

                if (isFile)
                {
                    // For files, we use the file id.
                    fileId = fileIdCounter;
                    fileIdCounter++;
                }
                else
                {
                    fileId = null;
                }

                int fileSize = int.Parse(character.ToString());

                // Add the file.
                this.blockList.Add(new FileBlock(fileId, fileSize));

                // Toggle the file flag.
                isFile = !isFile;
            }
        }
    }

    public long DefragAndGetChecksum()
    {
        this.Defrag();
        return this.GetChecksum();
    }

    /// <summary>
    /// Defragments the file list.
    /// </summary>
    private void Defrag()
    {
        // Start at the back of the disk and try to find space to move everything forward.
        for (int back = this.blockList.Count - 1; back > 0; back--)
        {
            // Check if the back block is empty.
            FileBlock backFile = this.blockList[back];
            if (backFile.IsEmpty)
            {
                // It is empty. Try again on the next file.
                continue;
            }

            // Find the first empty block from the front that has room for this block.
            for (int front = 0; front < back; front++)
            {
                FileBlock frontFile = this.blockList[front];

                // Check if this is an empty space that can fit this file.
                if (!frontFile.IsEmpty || frontFile.Size < backFile.Size)
                {
                    // Nope. Try again with the next block.
                    continue;
                }

                // We are now at a spot where the front file is empty
                // and big enough to fit the back file.
                // Shrink the empty file at the front,
                // swap the back file into the space of the front file,
                // and replace it with an empty file at the back.
                this.blockList[front].Size -= backFile.Size;
                this.blockList.Insert(front, new FileBlock(backFile.FileId, backFile.Size));
                backFile.FileId = null;
                break;
            }
        }

        // The blocklist should now be successfully defragmented.
    }

    /// <summary>
    /// Computes the checksum of the disk.
    /// </summary>
    /// <returns></returns>
    private long GetChecksum()
    {
        long checkSum = 0;
        // The checksum is computed by multiplying a positional index by the value at the index.
        int index = 0;
        foreach (FileBlock file in this.blockList)
        {
            // We only care about the filled files.
            if (file.IsEmpty)
            {
                // Increment the index and continue.
                index += file.Size;
                continue;
            }

            // Increase the checksum for each index in the file.
            for (int i = 0; i < file.Size; i++)
            {
                checkSum += index++ * file.FileId.Value;
            }
        }

        return checkSum;
    }
}
