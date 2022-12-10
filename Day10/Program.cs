using System.Diagnostics;
using System.Text;

var input = File.ReadAllLines("input.txt");
var inputLength = input.Length;

// Parsing
Span<int> xRegOverTime = stackalloc int[inputLength * 2];
Span<int> signalStrengthOverTime = stackalloc int[inputLength * 2];

var clockCycle = 1;
var xReg = 1;

for (int i = 0; i < inputLength; i++)
{
    if (input[i][0] == 'a')
    {
        IncrementClockCycle(in xRegOverTime, in signalStrengthOverTime, in xReg, ref clockCycle);
        IncrementClockCycle(in xRegOverTime, in signalStrengthOverTime, in xReg, ref clockCycle);
        
        // Fuck int.Parse!!
        var isNegative = false;
        var result = 0;
        for (var j = 5; j < input[i].Length; j++)
        {
            switch (input[i][j])
            {
                case '-':
                    isNegative = true;
                    break;
                default:
                    result *= 10;
                    result += input[i][j] - '0';
                    break;
            }
        }
        xReg += result * (isNegative ? -1 : 1);
    }
    else
    {
        IncrementClockCycle(in xRegOverTime, in signalStrengthOverTime, in xReg, ref clockCycle);
    }
}

// Part 1
var signalStrengthSum = signalStrengthOverTime[20]
    + signalStrengthOverTime[60]
    + signalStrengthOverTime[100]
    + signalStrengthOverTime[140]
    + signalStrengthOverTime[180]
    + signalStrengthOverTime[220];

Console.WriteLine($"Sum of the arbitrary 6 signal strengths: {signalStrengthSum}");

// Part 2
var sb = new StringBuilder(240 + (240 / 40));
for (int i = 0; i < 240; i++)
{
    var cycle = i % 40;
    var diff = xRegOverTime[i + 1] - cycle;
    sb.Append((diff <= 1 && diff >= -1) ? '#' : '.');
    if (cycle == 39) sb.AppendLine();
}
Console.WriteLine(sb);

// Helper method
static void IncrementClockCycle(in Span<int> xRegSpan, in Span<int> signalStrengthSpan, in int xReg, ref int clockCycle)
{
    xRegSpan[clockCycle] = xReg;
    signalStrengthSpan[clockCycle] = clockCycle * xReg;
    clockCycle++;
}