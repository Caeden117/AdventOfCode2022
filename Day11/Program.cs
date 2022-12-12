// Parsing
using System.Diagnostics;

var input = File.ReadAllLines("input.txt");
var monkeys = new List<Monkey>(input.Length / 7);
var lcm = 1ul;

for (var i = 0; i < input.Length; i += 7)
{
    var initialItems = new List<ulong>();
    var initialItemsStr = input[i + 1][18..].AsSpan();
    for (var j = 0; j < initialItemsStr.Length; j+= 4)
    {
        initialItems.Add(ParseUlong(initialItemsStr.Slice(j, 2)));
    }

    var operationSplit = input[i + 2][19..].Split(' ');
    Func<ulong, ulong> worryOperation = operationSplit[1] switch
    {
        "*" => (old) => (operationSplit[0] == "old" ? old : ParseUlong(operationSplit[0].AsSpan()))
            * (operationSplit[2] == "old" ? old : ParseUlong(operationSplit[2].AsSpan())),

        "+" => (old) => (operationSplit[0] == "old" ? old : ParseUlong(operationSplit[0].AsSpan()))
            + (operationSplit[2] == "old" ? old : ParseUlong(operationSplit[2].AsSpan())),

        _ => throw new UnreachableException("huh")
    };

    var worryTestAgainst = ParseUlong(input[i + 3][21..].AsSpan());
    lcm *= worryTestAgainst;
    var monkeyIdIfTrue = ParseInt(input[i + 4][29..].AsSpan());
    var monkeyIdIfFalse = ParseInt(input[i + 5][30..].AsSpan());

    int throwTest (ulong worry) => worry % worryTestAgainst == 0
        ? monkeyIdIfTrue 
        : monkeyIdIfFalse;

    monkeys.Add(new(initialItems, worryOperation, throwTest));
}

// Part 1 and 2
var inspectionCount = new ulong[monkeys.Count];

for (var round = 0; round < 10000; round++) // round < 20 for part 1, round < 10000 for part 2
{
    for (var monkeyId = 0; monkeyId < monkeys.Count; monkeyId++)
    {
        var monkey = monkeys[monkeyId];

        var itemCount = monkey.Items.Count;
        for (var item = 0; item < itemCount; item++)
        {
            var initalWorry = monkey.Items[0];

            // monkey inspects an item
            inspectionCount[monkeyId]++;

            var afterInspection = monkey.WorryOperation(initalWorry) % lcm;
            //afterInspection /= 3; // comment out for part 2
            
            var nextMonkey = monkey.ThrowTo(afterInspection);

            monkey.Items.RemoveAt(0);
            monkeys[nextMonkey].Items.Add(afterInspection);
        }
    }
}

var monkeyBusiness = inspectionCount.Order().TakeLast(2).Aggregate(1ul, (a, b) => a * b);
Console.WriteLine($"Level of monkey business: {monkeyBusiness}");

// Helper methods
// Fuck int.Parse!!
static int ParseInt(ReadOnlySpan<char> input)
{
    var result = 0;
    for (var i = 0; i < input.Length; i++)
    {
        result = result * 10 + input[i] - '0';
    }
    return result;
}

static ulong ParseUlong(ReadOnlySpan<char> input)
{
    var result = 0ul;
    for (var i = 0; i < input.Length; i++)
    {
        result = result * 10 + input[i] - '0';
    }
    return result;
}

// Data structures
record Monkey(List<ulong> Items, Func<ulong, ulong> WorryOperation, Func<ulong, int> ThrowTo);