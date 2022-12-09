using System.Diagnostics;
var input = await File.ReadAllLinesAsync("input.txt");

// Part 1
var pointsWithRespondingShape = 0;
for (int i = 0; i < input.Length; i++)
{
    var line = input[i];
    pointsWithRespondingShape += line[0] switch
    {
        'A' => line[2] switch
        {
            'X' => 1 + 3,
            'Y' => 2 + 6,
            'Z' => 3 + 0,
            _ => throw new UnreachableException("huh?")
        },
        'B' => line[2] switch
        {
            'X' => 1 + 0,
            'Y' => 2 + 3,
            'Z' => 3 + 6,
            _ => throw new UnreachableException("huh?")
        },
        'C' => line[2] switch
        {
            'X' => 1 + 6,
            'Y' => 2 + 0,
            'Z' => 3 + 3,
            _ => throw new UnreachableException("huh?")
        },
        _ => throw new UnreachableException("huh?")
    };
}

Console.WriteLine($"Sum of points from all games, given X/Y/Z are shapes to respond with: {pointsWithRespondingShape}");

// Part 2
var pointsWithDeterminedResult = 0;
for (int i = 0; i < input.Length; i++)
{
    var line = input[i];
    pointsWithDeterminedResult += line[0] switch
    {
        'A' => line[2] switch
        {
            'X' => 3 + 0,
            'Y' => 1 + 3,
            'Z' => 2 + 6,
            _ => throw new UnreachableException("huh?")
        },
        'B' => line[2] switch
        {
            'X' => 1 + 0,
            'Y' => 2 + 3,
            'Z' => 3 + 6,
            _ => throw new UnreachableException("huh?")
        },
        'C' => line[2] switch
        {
            'X' => 2 + 0,
            'Y' => 3 + 3,
            'Z' => 1 + 6,
            _ => throw new UnreachableException("huh?")
        },
        _ => throw new UnreachableException("huh?")
    };
}

Console.WriteLine($"Sum of points from all games, given X/Y/Z are the expected result of the games: {pointsWithDeterminedResult}");