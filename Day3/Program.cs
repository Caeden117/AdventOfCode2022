// Part 1
const string prioritiesTable = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
var input = await File.ReadAllTextAsync("input.txt");

var rucksacks = input.Split(Environment.NewLine).ToList();

var sumPriorities = rucksacks
    .Select(rucksack =>
    {
        var rucksackSize = rucksack.Length / 2;
        return new[] { rucksack[0..rucksackSize], rucksack[rucksackSize..] };
    })
    .SelectMany(compartment => compartment[0].Intersect(compartment[1]))
    .Sum(sharedItem => prioritiesTable.IndexOf(sharedItem) + 1);

Console.WriteLine($"Sum of all item type priorities is {sumPriorities}");

// Part 2
var sumGroupProorities = rucksacks
    .GroupBy(rucksack => rucksacks.IndexOf(rucksack) / 3)
    .Select(group => group.ToArray())
    .Where(group => group.Length == 3)
    .SelectMany(group => group[0].Intersect(group[1]).Intersect(group[2]))
    .Sum(groupBadge => prioritiesTable.IndexOf(groupBadge) + 1);

Console.WriteLine($"Sum of all group badge priorities is {sumGroupProorities}");