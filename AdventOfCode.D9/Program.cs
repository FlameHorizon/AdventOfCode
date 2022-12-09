using System.ComponentModel;

namespace AdventOfCode.D9
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
            Console.WriteLine("Simulate your complete hypothetical series of motions. "
                + "How many positions does the tail of the rope visit at least once?");

            string[] input = File.ReadAllLines("input.txt");

            IEnumerable<Move> moves = ParseMoves(input);
            var board = new Board();
            board.MoveHead(moves);

            var result = board.TailPositionsVisitedCount;
            Console.WriteLine(result);
        }

        public static IEnumerable<Move> ParseMoves(string[] moves)
        {
            return moves.Select(m => ParseMove(m));
        }

        public static Move ParseMove(string move) => new Move(move);

        public class Move
        {
            public MoveDirection Direction { get; init; }
            public int Distance { get; init; }
            public Program.Vector Vector { get; init; }

            public Move(string raw)
            {
                var split = raw.Split(" ");
                Direction = GetDirection(split[0]);
                Distance = int.Parse(split[1]);
                Vector = GetVector();
            }

            private Program.Vector GetVector()
            {
                return Direction switch
                {
                    MoveDirection.Right => new Program.Vector(Distance, 0),
                    MoveDirection.Up => new Program.Vector(0, Distance),
                    MoveDirection.Left => new Program.Vector(-Distance, 0),
                    MoveDirection.Down => new Program.Vector(0, -Distance),
                    _ => throw new ArgumentException("Cannot create vector.")
                };
            }

            public Move(MoveDirection direction, int distance)
            {
                Direction = direction;
                Distance = distance;
                Vector = GetVector();
            }

            private static MoveDirection GetDirection(string direction)
            {
                return direction switch
                {
                    "R" => MoveDirection.Right,
                    "U" => MoveDirection.Up,
                    "L" => MoveDirection.Left,
                    "D" => MoveDirection.Down,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction))
                };
            }
        }

        public enum MoveDirection
        {
            Right,
            Up,
            Left,
            Down
        }

        public class Board
        {
            public char[,] State { get; init; }
            public Program.Point HeadPosition { get; private set; }
            public Program.Point TailPosition { get; private set; }
            public int TailPositionsVisitedCount
            {
                get
                {
                    return _visited.Keys.Count;
                }
            }

            private Dictionary<string, int> _visited = new();

            public Board()
            {
                State = new char[,]
                {
                    { '.','.','.','.','.','.' },
                    { '.','.','.','.','.','.' },
                    { '.','.','.','.','.','.' },
                    { '.','.','.','.','.','.' },
                    { 'H','.','.','.','.','.' }
                };

                HeadPosition = new Program.Point(0, 0);
                TailPosition = new Program.Point(0, 0);
                _visited.Add(TailPosition.X + " " + TailPosition.Y, 1);
            }

            public void MoveHead(IEnumerable<Move> moves)
            {
                moves.ToList().ForEach(m => MoveHead(m));
            }

            public void MoveHead(Move move)
            {
                for (int i = 1; i <= move.Distance; i++)
                {
                    Vector headMoveVector = move.Direction switch
                    {
                        MoveDirection.Right => new Vector(1, 0),
                        MoveDirection.Up => new Vector(0, 1),
                        MoveDirection.Left => new Vector(-1, 0),
                        MoveDirection.Down => new Vector(0, -1),
                        _ => throw new InvalidEnumArgumentException()
                    };

                    HeadPosition = HeadPosition.Add(headMoveVector);

                    if (GetDistance(HeadPosition, TailPosition) >= 2)
                    {
                        Program.Vector tailMoveVector = headMoveVector.Inverse();
                        TailPosition = HeadPosition.Add(tailMoveVector);
                        _visited.TryAdd(TailPosition.X + " " + TailPosition.Y, 1);
                    }
                }
            }

            private static double GetDistance(Program.Point p1,
                Program.Point p2)
            {
                return Math.Sqrt(Math.Pow((p2.X - p1.X), 2)
                               + Math.Pow((p2.Y - p1.Y), 2));
            }
        }

        private static void Part2()
        {
            Console.WriteLine("Simulate your complete series of motions on a larger rope with ten knots. "
                 + "How many positions does the tail of the rope visit at least once");

            string[] input = File.ReadAllLines("input.txt");

            IEnumerable<Move> moves = ParseMoves(input);
            var board = new BigBoard();
            board.MoveHead(moves);

            var result = board.TailPositionsVisitedCount;
            Console.WriteLine(result);
        }

        public class BigBoard
        {
            public Program.Point HeadPosition { get; set; } = new Point();
            public Program.Point[] PiecesPoints { get; init; } = new Point[10];

            public BigBoard()
            {
                for (int i = 0; i < PiecesPoints.Length; i++)
                {
                    PiecesPoints[i] = new Program.Point();
                }
                HeadPosition = PiecesPoints[0];

                _visited.Add("0 0", 1);
            }

            private Dictionary<string, int> _visited = new();
            public int TailPositionsVisitedCount
            {
                get
                {
                    return _visited.Keys.Count;
                }
            }

            public void MoveHead(IEnumerable<Move> moves)
            {
                moves.ToList().ForEach(m => MoveHead(m));
            }

            public void MoveHead(Move move)
            {
                for (int i = 1; i <= move.Distance; i++)
                {
                    Vector headMoveVector = move.Direction switch
                    {
                        MoveDirection.Right => new Program.Vector(1, 0),
                        MoveDirection.Up => new Program.Vector(0, 1),
                        MoveDirection.Left => new Program.Vector(-1, 0),
                        MoveDirection.Down => new Program.Vector(0, -1),
                        _ => throw new InvalidOperationException("Can't move head in this direction."),
                    };

                    PiecesPoints[0] = PiecesPoints[0].Add(headMoveVector);

                    for (int j = 1; j < PiecesPoints.Length; j++)
                    {
                        var leadingKnot = PiecesPoints[j - 1];
                        var followingKnot = PiecesPoints[j];
                        var distance = (leadingKnot.X - followingKnot.X, leadingKnot.Y - followingKnot.Y);

                        Program.Vector moveVector = null!;
                        #region Find vector
                        if (distance == (2, 2))
                        {
                            moveVector = new Program.Vector(1, 1);
                        }
                        else if (distance == (-2, -2))
                        {
                            moveVector = new Program.Vector(-1, -1);
                        }
                        else if (distance == (-2, 2))
                        {
                            moveVector = new Program.Vector(-1, 1);
                        }
                        else if (distance == (2, -2))
                        {
                            moveVector = new Program.Vector(1, -1);
                        }
                        else if (distance is (int x3, -2))
                        {
                            moveVector = new Program.Vector(x3, -1);
                        }
                        else if (distance is (-2, int x4))
                        {
                            moveVector = new Program.Vector(-1, x4);
                        }
                        else if (distance is (2, int x2))
                        {
                            moveVector = new Program.Vector(1, x2);
                        }
                        else if (distance is (int x1, 2))
                        {
                            moveVector = new Program.Vector(x1, 1);
                        }
                        else
                        {
                            moveVector = new Program.Vector(0, 0);
                        }
                        #endregion

                        PiecesPoints[j] = PiecesPoints[j].Add(moveVector);
                    }
                    _visited.TryAdd(PiecesPoints[9].X + " " + PiecesPoints[9].Y, 1);
                }
            }

            private static double GetDistnace(Program.Point p1, Program.Point p2)
            {
                return Math.Sqrt(Math.Pow((p2.X - p1.X), 2)
                               + Math.Pow((p2.Y - p1.Y), 2));
            }
        }

        public class Point : IEquatable<Point>
        {
            public int X { get; private set; } = 0;
            public int Y { get; private set; } = 0;

            public Point() { }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Point Add(Vector vector)
            {
                return new Point(X + vector.X, Y + vector.Y);
            }

            public bool Equals(Point? other)
            {
                if (other == null)
                {
                    return false;
                }

                return other.X == X && other.Y == Y;
            }

            public override bool Equals(object obj) => Equals(obj as Point);

            public override int GetHashCode()
            {
                return 17 * X + 17 * Y;
            }
        }

        public class Vector : IEquatable<Vector>
        {
            public int X { get; private set; }
            public int Y { get; private set; }

            public Vector(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Equals(Vector? other)
            {
                if (other == null)
                {
                    return false;
                }

                return other.X == X && other.Y == Y;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Vector);
            }

            public Vector Inverse()
            {
                return new Vector(X * (- 1), Y * (- 1));
            }
        }
    }
}