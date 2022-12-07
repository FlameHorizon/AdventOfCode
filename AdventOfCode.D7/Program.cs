using System.Text.RegularExpressions;
using System.IO;

namespace AdventOfCode.D7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Part1();

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void Part1()
        {
            Console.WriteLine("Find all of the directories with a total size of at most 100000. "
                + "What is the sum of the total sizes of those directories?");

//            var input =
//@"$ cd /
//$ ls
//dir a
//14848514 b.txt
//8504156 c.dat
//dir d
//$ cd a
//$ ls
//dir e
//29116 f
//2557 g
//62596 h.lst
//$ cd e
//$ ls
//584 i
//$ cd ..
//$ cd ..
//$ cd d
//$ ls
//4060174 j
//8033020 d.log
//5626152 d.ext
//7214296 k";
            var input = System.IO.File.ReadAllText(@"data\input.txt");

            Directory root = ParseInput(input);

            int result = GetSize(root);
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

        private static bool IsMoveOut(string ln)
        {
            return ln.Contains("$ cd ..");
        }

        private static bool IsDirectory(string ln)
        {
            return ln.Contains("dir ");
        }

        private static bool IsChangeDirectory(string ln)
        {
            return Regex.IsMatch(ln, @"\$ cd [a-z]+");
        }

        private static bool IsFile(string ln)
        {
            return Regex.IsMatch(ln, "[0-9]+ [A-z]+");
        }

        private class Directory
        {
            public string Name { get; private set; }
            public Directory? Parent { get; private set; } = null;
            public List<Directory> Directories { get; private set; } = new List<Directory>();
            public List<File> Files { get; private set; } = new List<File>();

            public int Size => Files.Sum(f => f.Length) + Directories.Sum(d => d.Size);

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
                Files.Add(file);

                return file;
            }
        }

        private class File
        {
            public string Name { get; private set; }
            public int Length { get; private set; }

            public File(string name, int length)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Length = length;
            }
        }

        private static int GetSize(Directory root)
        {
            int size = 0;
            foreach (Directory dir in root.Directories)
            {
                size += GetSize(dir);
            }

            return root.Size <= 100000 ? size += root.Size : size;
        }
    }
}