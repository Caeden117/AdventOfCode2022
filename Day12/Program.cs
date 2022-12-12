// yes i am abusing the fact that I/O isnt measured in the benchmarks
using System.Runtime.CompilerServices;

var inputStr = File.ReadAllText("input.txt");
var inputLines = File.ReadAllLines("input.txt");
var xLength = inputLines[0].Length;
var yLength = inputLines.Length;

var startPosition = inputStr.IndexOf('S');
var endPosition = inputStr.IndexOf('E');

Span<bool> visitedPositions = stackalloc bool[inputStr.Length];

// Idea if I want to get rid of allocations: pool vectors using stackalloced arrays
var activePaths = new List<Vector2Int>() { GetPositionForArrayIdx(startPosition) };
visitedPositions[startPosition] = true;

// Part 1
var steps = 0;
var atEnd = false; 
while (true)
{
    var beginningCount = activePaths.Count;
    for (var i = 0; i < beginningCount; i++)
    {
        var path = activePaths[0];

        bool TestNewDirection(ref Span<bool> visitedPositions, int addX, int addY)
        {
            // Bounds checking
            if (path.X + addX < 0 || path.Y + addY < 0 || path.X + addX >= xLength || path.Y + addY >= yLength) return false;

            var hashedPosition = GetArrayIdxForPosition(path.X, path.Y);
            var hashedNewPosition = GetArrayIdxForPosition(path.X + addX, path.Y + addY);

            var strValue = inputStr[hashedPosition];
            if (strValue == 'S') strValue = 'a';

            var newStrValue = inputStr[hashedNewPosition];
            if (newStrValue == 'E') newStrValue = 'z';

            if (!visitedPositions[hashedNewPosition] && newStrValue - strValue <= 1)
            {
                if (hashedNewPosition == endPosition) return true;

                visitedPositions[hashedNewPosition] = true;
                activePaths!.Add(new(path.X + addX, path.Y + addY));
            }
            return false;
        }

        activePaths.RemoveAt(0);

        atEnd |= TestNewDirection(ref visitedPositions, 1, 0)
            || TestNewDirection(ref visitedPositions, -1, 0)
            || TestNewDirection(ref visitedPositions, 0, 1)
            || TestNewDirection(ref visitedPositions, 0, -1);
    }

    steps++;
    if (atEnd) break;
}

Console.WriteLine($"Reached end position in {steps} steps");

// Part 2 (will have to redefine this stuff)
visitedPositions.Clear();
activePaths.Clear();

for (var i = 0; i < inputStr.Length; i++)
{
    if (inputStr[i] == 'a')
    {
        activePaths.Add(GetPositionForArrayIdx(i));
        visitedPositions[i] = true;
    }
}

steps = 0;
atEnd = false;
while (true)
{
    var beginningCount = activePaths.Count;
    for (var i = 0; i < beginningCount; i++)
    {
        var path = activePaths[0];

        bool TestNewDirection(ref Span<bool> visitedPositions, int addX, int addY)
        {
            // Bounds checking
            if (path.X + addX < 0 || path.Y + addY < 0 || path.X + addX >= xLength || path.Y + addY >= yLength) return false;

            var hashedPosition = GetArrayIdxForPosition(path.X, path.Y);
            var hashedNewPosition = GetArrayIdxForPosition(path.X + addX, path.Y + addY);

            var strValue = inputStr[hashedPosition];
            if (strValue == 'S') strValue = 'a';

            var newStrValue = inputStr[hashedNewPosition];
            if (newStrValue == 'E') newStrValue = 'z';

            if (!visitedPositions[hashedNewPosition] && newStrValue - strValue <= 1)
            {
                if (hashedNewPosition == endPosition) return true;

                visitedPositions[hashedNewPosition] = true;
                activePaths!.Add(new(path.X + addX, path.Y + addY));
            }
            return false;
        }

        activePaths.RemoveAt(0);

        atEnd |= TestNewDirection(ref visitedPositions, 1, 0)
            || TestNewDirection(ref visitedPositions, -1, 0)
            || TestNewDirection(ref visitedPositions, 0, 1)
            || TestNewDirection(ref visitedPositions, 0, -1);
    }

    steps++;
    if (atEnd) break;
}

Console.WriteLine($"Optimized start position reached end position in {steps} steps");

// Helper methods
[MethodImpl(MethodImplOptions.AggressiveInlining)]
int GetArrayIdxForPosition(int x, int y) => (y * xLength) + x + (y * 2);

[MethodImpl(MethodImplOptions.AggressiveInlining)]
Vector2Int GetPositionForArrayIdx(int idx) => new(idx % (xLength + 2), idx / (xLength + 2));

// Data structures (modified from day 9)
struct Vector2Int
{
    public int X;
    public int Y;

    public Vector2Int(int x, int y, bool hashed = false)
    {
        if (!hashed)
        {
            X = x;
            Y = y;
        }
        else
        {

        }
    }
}