// Part 1
var input = await File.ReadAllLinesAsync("input.txt");
var treeHeightmapWidth = input[0].Length;
var treeHeightmapHeight = input.Length;

var visibleTrees = 0;

for (var y = 0; y < treeHeightmapHeight; y++)
    for (var x = 0; x < treeHeightmapWidth; x++)
    {
        var treeHeight = input[y][x];

        var isTreeVisible = x == 0 || y == 0 || x == treeHeightmapWidth - 1 || y == treeHeightmapHeight - 1
        || IsTreeBlocked(treeHeight, Enumerable.Range(0, x).Select(left => input[y][left]))
        || IsTreeBlocked(treeHeight, Enumerable.Range(x + 1, treeHeightmapWidth - x - 1).Select(right => input[y][right]))
        || IsTreeBlocked(treeHeight, Enumerable.Range(0, y).Select(up => input[up][x]))
        || IsTreeBlocked(treeHeight, Enumerable.Range(y + 1, treeHeightmapHeight - y - 1).Select(down => input[down][x]));

        if (isTreeVisible)
        {
            visibleTrees++;
        }
    }

bool IsTreeBlocked(char tree, IEnumerable<char> treesToEdge) => treesToEdge.Max() < tree;

Console.WriteLine($"There are {visibleTrees} visible trees.");

// Part 2
var maxScenicScore = 0;
for (var y = 0; y < treeHeightmapHeight; y++)
    for (var x = 0; x < treeHeightmapWidth; x++)
    {
        var treeHeight = input[y][x];

        var scenicScore = GetTreeViewDistance(treeHeight, Enumerable.Range(0, x).Select(left => input[y][left]).Reverse())
            * GetTreeViewDistance(treeHeight, Enumerable.Range(x + 1, treeHeightmapWidth - x - 1).Select(right => input[y][right]))
            * GetTreeViewDistance(treeHeight, Enumerable.Range(0, y).Select(up => input[up][x]).Reverse())
            * GetTreeViewDistance(treeHeight, Enumerable.Range(y + 1, treeHeightmapHeight - y - 1).Select(down => input[down][x]));

        if (scenicScore > maxScenicScore) maxScenicScore = scenicScore;
    }

int GetTreeViewDistance(char targetTree, IEnumerable<char> treesToEdge)
{
    var distance = 0;
    foreach (var tree in treesToEdge)
    {
        distance++;
        if (tree >= targetTree) break;
    }
    return distance;
}

Console.WriteLine($"The highest scenic score possible is {maxScenicScore}.");