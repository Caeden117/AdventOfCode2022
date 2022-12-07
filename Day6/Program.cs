const int messageLength = 14; // Change to 4 for part 1, 14 for part 2
using var input = File.OpenRead("input.txt");
var totalChars = 0;
var currentByte = 0;
var processingPackets = new List<HashSet<char>>(messageLength);

while ((currentByte = input.ReadByte()) > -1)
{
    var currentChar = (char)currentByte;
    if (!char.IsAscii(currentChar)) continue;
    totalChars++;
    for (var i = processingPackets.Count - 1; i >= 0; i--)
    {
        if (!processingPackets[i].Add(currentChar))
            processingPackets.RemoveAt(i);
        else if (processingPackets[i].Count == messageLength)
            goto complete; // xkcd 292
    }
    processingPackets.Add(new(messageLength) { currentChar });
}

complete: Console.WriteLine($"Start of packet detected after {totalChars} characters.");