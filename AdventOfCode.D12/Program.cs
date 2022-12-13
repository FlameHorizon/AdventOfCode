using System.Collections.ObjectModel;

namespace AdventOfCode.D12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Part1();

        }

        private static void Part1()
        {
            Console.WriteLine("What is the fewest steps required to move "
                + "from your current position to the location that should "
                + "get the best signal?");



            var result = 0;
            Console.WriteLine(result);
        }
    }

    public class Heightmap
    {
        private readonly int[,] _map;
        private readonly Dictionary<string, TreeNode<int>> _treeNodeDic = new();

        public (int x, int y) StartLocation { get; private set; }
        public TreeNode<int> StartNode { get; private set; }

        public (int x, int y) DestinationLocation { get; private set; }
        public TreeNode<int> DestinationNode { get; set; }

        public Heightmap(string input)
        {
            string[] split = input.Split(Environment.NewLine);
            int x = split.First().Length;
            int y = split.Length;

            _map = new int[x, y];

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    char value = split[j][i];
                    int height;

                    if (value == 'S')
                    {
                        height = 0;
                        StartLocation = (i, j);
                        StartNode = new TreeNode<int>(0);
                        _treeNodeDic.Add(GetNodeKey(i, j), StartNode);
                    }
                    else if (value == 'E')
                    {
                        height = 27;
                        DestinationLocation = (i, j);
                        DestinationNode = new TreeNode<int>(height);
                        _treeNodeDic.Add(string.Concat(i, ",", j), DestinationNode);
                    }
                    else
                    {
                        height = value - 96;
                    }

                    _map[i, j] = height;
                }
            }
        }

        public int GetHeight(int x, int y)
        {
            return _map[x, y];
        }

        public int GetHeight((int x, int y) position)
        {
            return GetHeight(position.x, position.y);
        }

        public TreeNode<int> GetNode(int x, int y)
        {
            string key = GetNodeKey(x, y);
            return _treeNodeDic[key];
        }

        private static string GetNodeKey(int x, int y)
        {
            return string.Concat(x, ",", y);
        }
    }

    public class TreeNode<T>
    {
        private readonly T _value;
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            _value = value;
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        public TreeNode<T> Parent { get; private set; }

        public T Value { get { return _value; } }

        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }
}