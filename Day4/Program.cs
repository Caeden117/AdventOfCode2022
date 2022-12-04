// Part 1
var input = await File.ReadAllTextAsync("input.txt");

var expandedPairs = input.Split(Environment.NewLine)
    .Select(pairs => pairs.Split(','))
    .Select(elf => elf
        .Select(range => range.Split('-'))
        .Select(endpoints => endpoints
            .Select(endpointsStr => int.Parse(endpointsStr))
            .ToArray())
        .Select(range => Enumerable.Range(range[0], range[1] - range[0] + 1))
        .ToArray());

var rendundantPairs = expandedPairs
    .Where(section =>
        section[0].All(x => section[1].Contains(x))
        || section[1].All(x => section[0].Contains(x)));

Console.WriteLine($"Pairs with redundant sections: {rendundantPairs.Count()}");

// Part 2
var overlappingPairs = expandedPairs
    .Where( it =>
    it[0].Any(x => it[1].Contains(x))
    || it[1].Any(x => it[0].Contains(x)));

Console.WriteLine($"Pairs with overlapping sections: {overlappingPairs.Count()}");