using System.Text;

namespace AdventOfCode2024.Days.Day09;

public class InputParser
{
    private List<int?> blockList;

    public InputParser()
    {
        StreamReader inputFile = new StreamReader("F:\\Projects\\AdventOfCode2024\\Days\\Day09\\input.txt");
        this.blockList = new List<int?>();

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

                int blockSize = int.Parse(character.ToString());

                // Add the appropriate files the appropriate number of times.
                for (int i = 0; i < blockSize; i++)
                {
                    this.blockList.Add(fileId);
                }

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

    private void Defrag()
    {
        int front = 0;
        int back = this.blockList.Count - 1;

        while (front < back)
        {
            // Check if the front block is empty.
            if (this.blockList[front] != null)
            {
                // Not an empty space. Iterate forward a step and try again.
                front++;
                continue;
            }

            // Check if the back block is empty.
            if (this.blockList[back] == null)
            {
                // It is empty. Iterate back a step and try again.
                back--;
                continue;
            }

            // We are now at a spot where the front block is empty
            // and the back block is not empty.
            // Swap the back block into the space of the front block,
            // and replace it with an empty marker.
            this.blockList[front] = this.blockList[back];
            this.blockList[back] = null;
        }

        // The blocklist should now be successfully defragmented.
    }

    private long GetChecksum()
    {
        long checkSum = 0;
        // The checksum is computed by multiplying a positional index by the value at the index.
        for (int index = 0; index < this.blockList.Count; index++)
        {
            // We only care about the numbers, which should all be at the front.
            int? fileId = this.blockList[index];
            if (!fileId.HasValue)
            {
                break;
            }

            checkSum += index * fileId.Value;
        }

        return checkSum;
    }
}
