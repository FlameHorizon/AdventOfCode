using static AdventOfCode.D10.Program;

namespace AdventOfCode.D10.Tests
{
    public class SystemTests
    {
        [Fact]
        public void InitSystemTest()
        {
            var sys = new Program.System();
            sys.AddOperation(new Operation(OperationType.Add, 15));

            string actual = sys.Monitor.CurrentLine;

            Assert.NotEmpty(sys.Cpu.Operations);
            Assert.Empty(actual);
        }

        [Fact]
        public void TickTest()
        {
            var sys = new Program.System();

            sys.AddOperation(new Operation(OperationType.Add, 15));
            sys.AddOperation(new Operation(OperationType.Add, -11));
            sys.AddOperation(new Operation(OperationType.Add, 6));
            sys.AddOperation(new Operation(OperationType.Add, -3));
            sys.AddOperation(new Operation(OperationType.Add, 5));
            sys.AddOperation(new Operation(OperationType.Add, -1));
            sys.AddOperation(new Operation(OperationType.Add, -8));
            sys.AddOperation(new Operation(OperationType.Add, 13));
            sys.AddOperation(new Operation(OperationType.Add, 4));
            sys.AddOperation(new Operation(OperationType.NoOperation));
            sys.AddOperation(new Operation(OperationType.Add, -1));

            var ticksRemaining = sys.Cpu.TicksRemaining;
            sys.Tick(ticksRemaining);

            string actual = sys.Monitor.CurrentLine;

            Assert.Equal("##..##..##..##..##..#", actual);
        }

        [Fact]
        public void ScreenlinesTest()
        {
            var input =
@"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";
            IEnumerable<Operation> operations = input
                .Split(Environment.NewLine)
                .Select(ln => new Operation(ln));

            var sys = new Program.System();
            sys.AddOperations(operations);

            var ticksRemaining = sys.Cpu.TicksRemaining;
            sys.Tick(ticksRemaining);

            Assert.Equal("##..##..##..##..##..##..##..##..##..##..", sys.Monitor.Screenlines.First());
            Assert.Equal("###...###...###...###...###...###...###.", sys.Monitor.Screenlines.Second());
            Assert.Equal("####....####....####....####....####....", sys.Monitor.Screenlines[2]);
            Assert.Equal("#####.....#####.....#####.....#####.....", sys.Monitor.Screenlines[3]);
            Assert.Equal("######......######......######......####", sys.Monitor.Screenlines[4]);
            Assert.Equal("#######.......#######.......#######.....", sys.Monitor.Screenlines[5]);
        }
    }
}
