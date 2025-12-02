using System.ComponentModel;

namespace AdventOfCode.D10
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
            Console.WriteLine("Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles. "
                + "What is the sum of these six signal strengths?");

            var input = File.ReadAllLines(@"input.txt");

            IEnumerable<Operation> instructions = input
                .Select(ln => new Operation(ln));

            var cpu = new Processor();
            cpu.AddInstrunctions(instructions);
            cpu.Tick(cpu.TicksRemaining);

            var result = cpu.SignalStrength.Sum();
            Console.WriteLine(result);
        }

        private static void Part2()
        {
            Console.WriteLine("Render the image given by your program. "
                + "What eight capital letters appear on your CRT?");

            var input = File.ReadAllLines(@"input.txt");

            IEnumerable<Operation> instructions = input
                .Select(ln => new Operation(ln));

            var sys = new Program.System();
            sys.AddOperations(instructions);
            sys.Tick(sys.Cpu.TicksRemaining);
            sys.Monitor.Screenlines.ForEach(s => Console.WriteLine(s));
        }

        public class Operation
        {
            public OperationType Type { get; init; }
            public int DurationInCycles { get; init; }
            public int Value { get; init; }
            public int TicksLeft { get; set; }

            public Operation(string raw)
            {
                string[] split = raw.Split();
                Type = split[0] switch
                {
                    "noop" => OperationType.NoOperation,
                    "addx" => OperationType.Add,
                    _ => throw new ArgumentException("Operation can't be handled.")
                };

                if (Type == OperationType.Add)
                {
                    Value = int.Parse(split[1]);
                }

                DurationInCycles = GetCycleCountForOperation(Type);
                TicksLeft = DurationInCycles;
            }

            private static int GetCycleCountForOperation(OperationType type)
            {
                return type switch
                {
                    OperationType.Add => 2,
                    OperationType.NoOperation => 1,
                    _ => throw new InvalidEnumArgumentException(nameof(type))
                };
            }

            public Operation(OperationType type, int value = 0)
            {
                Type = type;

                if (Type == OperationType.NoOperation && value != 0)
                {
                    throw new ArgumentException("No operation instruction can't have any value.");
                }

                Value = value;
                DurationInCycles = GetCycleCountForOperation(Type);
                TicksLeft = DurationInCycles;
            }
        }

        public enum OperationType
        {
            Add,
            NoOperation
        }

        public class Processor
        {
            public Queue<Operation> Operations { get; init; } = new Queue<Operation>();
            public int SpriteHorizontalPosition { get; private set; } = 1;
            public Operation? CurrentOperation { get; private set; } = null;
            public Operation? PreviousOperation { get; private set; } = null;
            public int TicksCount { get; private set; } = 1;
            public List<int> SignalStrength { get; private set; } = new List<int>();
            public int TicksRemaining { get; private set; } = 0;

            public void AddInstrunctions(IEnumerable<Operation> instructions)
            {
                instructions.ForEach(i => AddOperation(i));
            }

            public void AddOperation(Operation instr)
            {
                Operations.Enqueue(instr);
                TicksRemaining += instr.TicksLeft;
            }

            public void Tick(int count)
            {
                if (count <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(count),
                        "Count must be positive value.");
                }

                Enumerable.Range(1, count).ForEach(_ => Tick());
            }

            public void Tick()
            {
                // If current operations is empty, see if there is something to load in queue.
                if (CurrentOperation is null)
                {
                    if (Operations.Count > 0)
                    {
                        CurrentOperation = Operations.Dequeue();
                    }
                    else
                    {
                        return;
                    }
                }

                CurrentOperation.TicksLeft--;
                TicksRemaining--;
                TicksCount++;

                if (CurrentOperation.TicksLeft <= 0)
                {
                    if (CurrentOperation.Type == OperationType.Add)
                    {
                        SpriteHorizontalPosition += CurrentOperation.Value;
                    }

                    PreviousOperation = CurrentOperation;
                    CurrentOperation = null;
                }

                if (new int[] { 20, 60, 100, 140, 180, 220 }.Contains(TicksCount))
                {
                    SignalStrength.Add(SpriteHorizontalPosition * TicksCount);
                }
            }
        }

        public class Monitor
        {
            public (int start, int end) SpritePosition { get; private set; }
            public (int x, int y) PixelPosition { get; private set; }
            public string CurrentLine { get; private set; } = string.Empty;
            public List<string> Screenlines { get; private set; } = new List<string>();

            private int _ticksCount = 1;

            public Monitor()
            {
                SpritePosition = (0, 2);
                PixelPosition = (0, 0);
            }

            public void MoveSprite(int offset)
            {
                SpritePosition = (SpritePosition.start + offset,
                                  SpritePosition.end + offset);
            }

            public void Tick()
            {
                if (new int[] { 40, 80, 120, 160, 200, 240 }.Contains(_ticksCount))
                {
                    PixelPosition = (0, PixelPosition.y + 1);
                    Screenlines.Add(CurrentLine);
                    CurrentLine = string.Empty;
                }
                else
                {
                    PixelPosition = (PixelPosition.x + 1, PixelPosition.y);
                }

                _ticksCount++;
            }

            public bool ShouldDrawLitPixel()
            {
                return PixelPosition.x <= SpritePosition.end
                    && PixelPosition.x >= SpritePosition.start;
            }

            public void Draw()
            {
                CurrentLine += ShouldDrawLitPixel() ? "#" : ".";
            }

            public void SetSpritePosition(int horizontalPosition)
            {
                const int MaxSpritePosition = 40;
                if (horizontalPosition > MaxSpritePosition)
                {
                    throw new ArgumentOutOfRangeException(nameof(horizontalPosition),
                        $"Index can't be greater than 40. Value {horizontalPosition}");
                }

                const int MinSpritePosition = -1;
                if (horizontalPosition < MinSpritePosition)
                {
                    throw new ArgumentOutOfRangeException(nameof(horizontalPosition),
                        $"Index can't be smaller than -1. Value {horizontalPosition}");
                }

                SpritePosition = (horizontalPosition - 1, horizontalPosition + 1);
            }
        }

        public class System
        {
            public Processor Cpu { get; private set; } = new Processor();
            public Monitor Monitor { get; private set; } = new Monitor();

            public void AddOperations(IEnumerable<Operation> operations)
            {
                operations.ForEach(i => AddOperation(i));
            }

            public void AddOperation(Operation operation) => Cpu.AddOperation(operation);

            public void Tick(int count)
            {
                if (count <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(count),
                        "Count must be positive value.");
                }

                Enumerable.Range(1, count).ForEach(_ => Tick());
            }

            public void Tick()
            {
                Monitor.SetSpritePosition(Cpu.SpriteHorizontalPosition);
                Monitor.Draw();
                Cpu.Tick();
                Monitor.Tick();
            }
        }
    }
}