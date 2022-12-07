using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Immutable;

namespace AdventOfCode.D7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Part1();
            Part2();

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void Part1()
        {
            Console.WriteLine("Find all of the directories with a total size of at most 100000. "
                + "What is the sum of the total sizes of those directories?");

            var input = System.IO.File.ReadAllText(@"data\input.txt");

            Directory root = ParseInput(input);

            int result = GetSizes(root).Where(x => x <= 100000).Sum();
            Console.WriteLine(result);
        }

        private static Directory ParseInput(string input)
        {
            var root = new Directory("/");
            Directory curr = root;
            foreach (string ln in input.Split(Environment.NewLine))
            {
                if (ln == "$ cd /")
                    continue;

                if (ln == "$ ls")
                    continue;

                if (IsDirectory(ln))
                {
                    string name = ln.Split(" ")[1];
                    curr.CreateSubdirectory(name);
                    continue;
                }

                if (IsFile(ln))
                {
                    string[] split = ln.Split(" ");
                    curr.CreateFile(name: split[1], length: int.Parse(split[0]));
                    continue;
                }

                if (IsChangeDirectory(ln))
                {
                    string name = ln.Split(" ")[2];
                    curr = curr.Directories.SingleOrDefault(d => d.Name == name);
                    continue;
                }

                if (IsMoveOut(ln))
                {
                    curr = curr.Parent != null
                        ? curr.Parent
                        : throw new InvalidOperationException($"Can't move out from {curr.Name} "
                            + $"directory because it doesn't have parent directory.");
                    continue;
                }
            }

            return root;
        }

        private static bool IsMoveOut(string ln) => ln.Contains("$ cd ..");

        private static bool IsDirectory(string ln) => ln.Contains("dir ");

        private static bool IsChangeDirectory(string ln) => Regex.IsMatch(ln, @"\$ cd [a-z]+");

        private static bool IsFile(string ln) => Regex.IsMatch(ln, "[0-9]+ [A-z]+");

        private class Directory
        {
            public string Name { get; private set; }
            public Directory? Parent { get; private set; } = null;
            public List<Directory> Directories { get; private set; } = new List<Directory>();
            public IReadOnlyList<File> Files => _files.ToImmutableList();
            public int Size => Files.Sum(f => f.Length) + Directories.Sum(d => d.Size);

            private readonly List<File> _files = new();

            public Directory(string name)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }

            public Directory CreateSubdirectory(string name)
            {
                var dir = new Directory(name)
                {
                    Parent = this
                };
                Directories.Add(dir);

                return dir;
            }

            public File CreateFile(string name, int length)
            {
                var file = new File(name, length);
                _files.Add(file);

                return file;
            }
        }

        private class File
        {
            public string Name { get; init; }
            public int Length { get; init; }

            public File(string name, int length)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Length = length;
            }
        }

        private static List<int> GetSizes(Directory root)
        {
            var sizes = new List<int>();
            foreach (Directory dir in root.Directories)
            {
                sizes.AddRange(GetSizes(dir));
            }

            sizes.Add(root.Size);
            return sizes;
        }

        private static void Part2()
        {
            Console.WriteLine("Find the smallest directory that, if deleted, "
                + "would free up enough space on the file system to run the update. "
                + "What is the total size of that directory?");

            var input = System.IO.File.ReadAllText(@"data\input.txt");

            Directory root = ParseInput(input);
            int spaceInUse = root.Size;

            const int totalSpace = 70000000;
            int unsedSpace = totalSpace - spaceInUse;

            const int requiredSpaceForUpdate = 30000000;
            int additionalSpaceRequiredForUpdate = Math.Abs(unsedSpace - requiredSpaceForUpdate);

            List<int> sizes = GetSizes(root);
            int result = sizes.Where(x => x >= additionalSpaceRequiredForUpdate).Min();
            Console.WriteLine(result);
        }
    }
}