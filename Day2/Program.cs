// Part 1
var input = await File.ReadAllTextAsync("input.txt");

var matches = input
    .Split(Environment.NewLine)
    .Select(it => it.Split(' '))
    .Select(it => (it[0], it[1]));

var pointsWithRespondingShape = matches.Select(match => match switch
{
    ("A", "X") => 1 + 3,
    ("A", "Y") => 2 + 6,
    ("A", "Z") => 3 + 0,
    ("B", "X") => 1 + 0,
    ("B", "Y") => 2 + 3,
    ("B", "Z") => 3 + 6,
    ("C", "X") => 1 + 6,
    ("C", "Y") => 2 + 0,
    ("C", "Z") => 3 + 3,
    _ => throw new System.Diagnostics.UnreachableException("huh?")
});

Console.WriteLine($"Sum of points from all games, given X/Y/Z are shapes to respond with: {pointsWithRespondingShape.Sum()}");

// Part 2
var pointsWithDeterminedResult = matches.Select(match => match switch
{
    ("A", "X") => 3 + 0,
    ("A", "Y") => 1 + 3,
    ("A", "Z") => 2 + 6,
    ("B", "X") => 1 + 0,
    ("B", "Y") => 2 + 3,
    ("B", "Z") => 3 + 6,
    ("C", "X") => 2 + 0,
    ("C", "Y") => 3 + 3,
    ("C", "Z") => 1 + 6,
    _ => throw new System.Diagnostics.UnreachableException("huh?")
});

Console.WriteLine($"Sum of points from all games, given X/Y/Z are the expected result of the games: {pointsWithDeterminedResult.Sum()}");