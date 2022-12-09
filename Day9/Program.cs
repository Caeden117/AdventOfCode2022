using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

var inputSpan = File.ReadAllLines("input.txt").AsSpan();

const int tailLength = 9; // Change to 1 for part 1, 9 for part 2
HashSet<int> visitedPositions = new(inputSpan.Length);
Span<Vector2Int> rope = stackalloc Vector2Int[tailLength + 1];

for (var line = 0; line < inputSpan.Length; line++) {
    var currentLine = inputSpan[line];

    // Get around int.Parse by manually parsing the amount ourselves
    var movementAmount = currentLine.Length == 4
        ? ((currentLine[2] - '0') * 10) + currentLine[3] - '0'
        : currentLine[2] - '0';

    var direction = currentLine[0] switch
    {
        'R' => new Vector2Int(1, 0),
        'L' => new Vector2Int(-1, 0),
        'U' => new Vector2Int(0, 1),
        'D' => new Vector2Int(0, -1),
        _ => throw new UnreachableException("huh")
    };

    for (var i = 0; i < movementAmount; i++)
    {
        rope[0] += direction;

        for (var j = 0; j < tailLength; j++)
        {
            var distance = rope[j] - rope[j + 1];
            if (distance.Length > 1)
            {
                rope[j + 1] += distance.Direction;
            }
        }

        visitedPositions.Add(rope[^1].GetHashCode());
    }
}

Console.WriteLine($"Tail has visited {visitedPositions.Count} unique locations.");

// Data structures
struct Vector2Int
{
    public int Length => (X * X + Y * Y) >> 1;
    public Vector2Int Direction => new(Math.Sign(X), Math.Sign(Y));

    public int X;
    public int Y;

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2Int operator +(Vector2Int left, Vector2Int right) => new(left.X + right.X, left.Y + right.Y);

    public static Vector2Int operator -(Vector2Int left, Vector2Int right) => new(left.X - right.X, left.Y - right.Y);

    public override int GetHashCode()
    {
        var hash = Math.Abs(X) << 16;
        hash += Math.Abs(Y) << 2;
        hash += (Math.Sign(X) >= 0 ? 1 : 0) << 1;
        return hash + (Math.Sign(Y) >= 0 ? 1 : 0);
    }
}