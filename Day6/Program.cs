const int messageLength = 14; // Change to 4 for part 1, 14 for part 2
using var input = File.OpenRead("input.txt");
var totalChars = 0;
var currentChar = 0;
var bitfield = 0u;
var i = 0;
Span<int> processing = stackalloc int[(int)input.Length];

while ((currentChar = input.ReadByte()) > -1)
{
    if (currentChar > 'z') continue;
    processing[totalChars] = currentChar;
    totalChars++;
    if (totalChars >= messageLength)
    {
        bitfield = 0;
        
        for (i = totalChars - messageLength; i < totalChars; i++) bitfield |= 1u << (processing[i] - 'a');

        if (System.Numerics.BitOperations.PopCount(bitfield) == messageLength) break;
    }
}

Console.WriteLine($"Start of packet detected after {totalChars} characters.");