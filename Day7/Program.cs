// Parsing
using var rawInput = File.OpenRead("input.txt");
using var input = new StreamReader(rawInput);

DirInfo root = new("/", null);
DirInfo currentDir = root;
string? currentCommand = string.Empty;
while ((currentCommand = await input.ReadLineAsync()) != null)
{
    // We're about to do some serious bullshit
    switch (currentCommand[0])
    {
        case '$':
            // We only need to do command processing for cd
            if (currentCommand[2] == 'c')
            {
                currentDir = currentCommand[2] switch // Jump to root, dir parent, or dir child
                {
                    '/' => root,
                    _ => currentDir.ChildDirs.Find(dir => dir.Name == currentCommand[5..]) ?? currentDir.Parent ?? root,
                };
            }
            break;
        case 'd':
            // This is always a directory within an 'ls' command. Let's pre-create and load this dir
            currentDir.ChildDirs.Add(new(currentCommand[4..], currentDir));
            break;
        default:
            // With our given input, this will always be a file within an 'ls' command
            var commandComponents = currentCommand.Split(' ');
            currentDir.Files.Add(new(commandComponents[1], long.Parse(commandComponents[0])));
            break;
    }
}

// Part 1
var sumOfSmallDirs = 0L;
void IterateAndAddSmallDirSizes(DirInfo dir)
{
    var size = dir.Size;
    if (size <= 100_000) sumOfSmallDirs += size;
    dir.ChildDirs.ForEach(IterateAndAddSmallDirSizes);
}
IterateAndAddSmallDirSizes(root);

Console.WriteLine($"Size sum of small directories: {sumOfSmallDirs}");

// Part 2
var usedSpace = 70_000_000L - root.Size;
var cleanupTarget = 30_000_000L;
var deletionSize = 70_000_000L;
void IterateAndCheckDirForCleanup(DirInfo dir)
{
    var size = dir.Size;
    if (usedSpace + size >= cleanupTarget && size < deletionSize) deletionSize = size;
    dir.ChildDirs.ForEach(IterateAndCheckDirForCleanup);
}
IterateAndCheckDirForCleanup(root);

Console.WriteLine($"Total size of the largest directory within cleanup target: {deletionSize}");

// Data structures
record DirInfo(string Name, DirInfo? Parent)
{
    public long Size => ChildDirs.Sum(dir => dir.Size) + Files.Sum(file => file.Size);

    internal List<DirInfo> ChildDirs = new();
    internal List<FileInfo> Files = new();
}

record FileInfo(string Name, long Size);