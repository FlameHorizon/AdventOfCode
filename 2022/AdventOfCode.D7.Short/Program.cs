using System.Text.RegularExpressions;

string input = System.IO.File.ReadAllText(@"data\input.txt");
Directory root = ParseInput(input);
List<int> sizes = GetSizes(root);
Console.WriteLine(sizes.Where(x => x <= 100000).Sum());

const int requiredSpaceForUpdate = 30000000;
const int totalSpace = 70000000;
int additionalSpaceRequiredForUpdate = Math.Abs(totalSpace - requiredSpaceForUpdate - root.Size);

Console.WriteLine(sizes.Where(x => x >= additionalSpaceRequiredForUpdate).Min());

static Directory ParseInput(string input)
{
    var root = new Directory("/", null);
    Directory curr = root;
    foreach (string ln in input.Split(Environment.NewLine))
    {
        if (ln.Contains("dir "))
            curr.CreateSubdirectory(name: ln.Split(" ")[1]);

        else if (Regex.IsMatch(ln, "[0-9]+ [A-z]+"))
            curr.CreateFile(name: ln.Split(" ")[1], length: int.Parse(ln.Split(" ")[0]));

        else if (Regex.IsMatch(ln, @"\$ cd [a-z]+"))
            curr = curr.Directories.SingleOrDefault(d => d.Name == ln.Split(" ")[2]);

        else if (ln.Contains("$ cd .."))
            curr = curr.Parent;
    }

    return root;
}

static List<int> GetSizes(Directory root)
{
    var sizes = new List<int>();
    root.Directories.ForEach(x => sizes.AddRange(GetSizes(x)));

    sizes.Add(root.Size);
    return sizes;
}

class Directory
{
    public string Name { get; private set; }
    public Directory? Parent { get; private set; } = null;
    public List<Directory> Directories { get; private set; } = new List<Directory>();
    public int Size => _files.Sum(f => f.Length) + Directories.Sum(d => d.Size);

    private readonly List<File> _files = new();

    public Directory(string name, Directory? parent) { Name = name; Parent = parent; }

    public void CreateSubdirectory(string name) => Directories.Add(new Directory(name, this));

    public void CreateFile(string name, int length) => _files.Add(new File(name, length));
}

class File
{
    public string Name { get; init; }
    public int Length { get; init; }

    public File(string name, int length) { Name = name; Length = length; }
}