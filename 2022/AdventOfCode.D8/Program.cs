using System.Linq;

namespace AdventOfCode.D8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            Console.WriteLine("Consider your map; how many trees are visible from outside the grid?");

            string[] input = File.ReadAllLines(@"input.txt");

            var forest = new Forest(input);
            var result = forest.GetVisibleTreeCount();

            Console.WriteLine(result);
        }

        private class Forest
        {
            private Tree[,] _trees;
            public int X { get; } = 0;
            public int Y { get; } = 0;

            public Forest(string[] raw)
            {
                X = raw.Length;
                Y = raw[0].Length;

                _trees = new Tree[X, Y];
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        int h = int.Parse(raw[i][j].ToString());
                        _trees[i, j] = new Tree(h, i, j, this);
                    }
                }
            }

            public Tree GetTree(int x, int y)
            {
                return _trees[x, y];
            }

            public int GetVisibleTreeCount()
            {
                var output = 0;
                for (int i = 1; i < X - 1; i++)
                {
                    for (int j = 1; j < Y - 1; j++)
                    {
                        if (_trees[i, j].IsVisible())
                        {
                            output++;
                        }
                    }

                }
                return output + 2 * X + (2 * (Y - 2));
            }

            public IEnumerable<int> GetScenicScore()
            {
                var output = new List<int>();
                for (int i = 0; i < X - 1; i++)
                {
                    for (int j = 0; j < Y - 1; j++)
                    {
                        output.Add(_trees[i, j].GetScenicScore());
                    }

                }
                return output;
            }
        }

        private class Tree
        {
            public int Height { get; }
            public int X { get; }
            public int Y { get; }
            public Forest Forest { get; }

            public Tree(int height, int x, int y, Forest forest)
            {
                Height = height;
                X = x;
                Y = y;
                Forest = forest;
            }

            public bool IsVisible()
            {
                return IsTopVisible() || IsRightVisible() || IsDownVisible() || IsLeftVisible();
            }

            private bool IsTopVisible()
            {
                List<int> valuesAbove = new List<int>();

                for (int i = X - 1; i >= 0; i--)
                {
                    valuesAbove.Add(Forest.GetTree(i, Y).Height);
                }

                return valuesAbove.Max() < Height;
            }

            private bool IsRightVisible()
            {
                List<int> valuesToTheRight = new List<int>();

                for (int i = Y + 1; i < Forest.Y; i++)
                {
                    valuesToTheRight.Add(Forest.GetTree(X, i).Height);
                }

                return valuesToTheRight.Max() < Height;
            }

            private bool IsDownVisible()
            {
                List<int> valuesBelow = new List<int>();

                for (int i = X + 1; i < Forest.X; i++)
                {
                    valuesBelow.Add(Forest.GetTree(i, Y).Height);
                }

                return valuesBelow.Max() < Height;
            }

            private bool IsLeftVisible()
            {
                List<int> valuesToTheLeft = new List<int>();

                for (int i = Y - 1; i >= 0; i--)
                {
                    valuesToTheLeft.Add(Forest.GetTree(X, i).Height);
                }

                return valuesToTheLeft.Max() < Height;

            }

            public int GetScenicScore()
            {
                var scoreTop = 0;

                for (int i = X - 1; i >= 0; i--)
                {
                    int neighbourTreeHeight = Forest.GetTree(i, Y).Height;
                    if (neighbourTreeHeight < Height)
                    {
                        scoreTop++;
                    }
                    else if (neighbourTreeHeight >= Height)
                    {
                        scoreTop++;
                        break;
                    }
                }

                int scoreRight = 0;
                for (int i = Y + 1; i < Forest.Y; i++)
                {
                    int neighbourTreeHeight = Forest.GetTree(X, i).Height;
                    if (neighbourTreeHeight < Height)
                    {
                        scoreRight++;
                    }
                    else if (neighbourTreeHeight >= Height)
                    {
                        scoreRight++;
                        break;
                    }
                }

                int scoreDown = 0;
                for (int i = X + 1; i < Forest.X; i++)
                {
                    int neighbourTreeHeight = Forest.GetTree(i, Y).Height;
                    if (neighbourTreeHeight < Height)
                    {
                        scoreDown++;
                    }
                    else if (neighbourTreeHeight >= Height)
                    {
                        scoreDown ++;
                        break;
                    }
                }

                int scoreLeft = 0;
                for (int i = Y - 1; i >= 0; i--)
                {
                    int neighbourTreeHeight = Forest.GetTree(X, i).Height;
                    if (neighbourTreeHeight < Height)
                    {
                        scoreLeft++;
                    }
                    else if (neighbourTreeHeight >= Height)
                    {
                        scoreLeft++;
                        break;
                    }
                }

                return scoreTop * scoreRight * scoreDown * scoreLeft;

            }
        }

        private static void Part2()
        {
            Console.WriteLine("Consider each tree on your map. "
                + "What is the highest scenic score possible for any tree?");

            string[] input = File.ReadAllLines(@"input.txt");
            var forest = new Forest(input);

            var result = forest.GetScenicScore().Max();
            Console.WriteLine(result);
        }
    }
}