// Part 1
var input = await File.ReadAllTextAsync("input.txt");
var crateAndInstructions = input.Split(Environment.NewLine + Environment.NewLine);
var crates = new List<char>[9];

var crateLines = crateAndInstructions[0].Split(Environment.NewLine);
foreach (var crateLine in crateLines)
{
    for (var i = 0; i < crateLine.Length; i++)
    {
        var ch = crateLine[i];

        if (char.IsLetter(ch))
        {
            int idx = i / 4;
            crates[idx] ??= new();
            crates[idx].Add(ch);
        }
    }
}

foreach (var crate in crates)
{
    crate.Reverse();
}

var instructions = crateAndInstructions[1].Split(Environment.NewLine);

foreach (var instruction in instructions)
{
    var splitInstructions = instruction.Split(' ');

    var crateNumbers = int.Parse(splitInstructions[1]);
    var fromCrate = crates[int.Parse(splitInstructions[3]) - 1];
    var toCrate = crates[int.Parse(splitInstructions[5]) - 1];

    for (int i = crateNumbers; i > 0; i--)
    {
        var crate = fromCrate[fromCrate.Count - i];
        fromCrate.RemoveAt(fromCrate.Count - i);
        toCrate.Add(crate);
    }
}

foreach (var crate in crates)
{
    Console.Write(crate.Count == 0 ? ' ' : crate.Last());
}
