// Part 1
var input = await File.ReadAllTextAsync("input.txt");

var totalCaloriesPerElf = input
    .Split(Environment.NewLine + Environment.NewLine)
    .Select(it => it.Split(Environment.NewLine).Where(it => !string.IsNullOrWhiteSpace(it)))
    .Select(it => it.Select(str => int.Parse(str)).Sum())
    .OrderByDescending(it => it)
    .ToList();

Console.WriteLine($"Highest calorie count carried by an elf: {totalCaloriesPerElf.Max()}");

// Part 2
var sum = totalCaloriesPerElf.Take(3).Sum();

Console.WriteLine($"Sum of top 3 highest calorie counts: {sum}");