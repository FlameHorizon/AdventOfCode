using System;
using System.Xml.Serialization;
using static AdventOfCode.D10.Program;
using System.Linq;

namespace AdventOfCode.D10.Tests
{
    public class ProcessorTests
    {
        [Fact]
        public void InitProcessorTest()
        {
            var cpu = new Processor();

            Assert.Empty(cpu.Operations);
            Assert.Equal(1, cpu.SpriteHorizontalPosition);
            Assert.Equal(1, cpu.TicksCount);
        }

        [Fact]
        public void AddOperationTest()
        {
            var cpu = new Processor();
            var instr1 = new Operation(OperationType.NoOperation);

            cpu.AddOperation(instr1);

            Assert.Single(cpu.Operations);
            Assert.Equal(instr1, cpu.Operations.First());
            Assert.Equal(1, cpu.TicksRemaining);

            var instr2 = new Operation(OperationType.Add, 3);

            cpu.AddOperation(instr2);

            Assert.Equal(2, cpu.Operations.Count);
            Assert.Equal(instr2, cpu.Operations.Second());
            Assert.Equal(3, cpu.Operations.Second().Value);
            Assert.Equal(3, cpu.TicksRemaining);

            var instr3 = new Operation(OperationType.Add, -5);

            cpu.AddOperation(instr3);

            Assert.Equal(3, cpu.Operations.Count);
            Assert.Equal(instr3, cpu.Operations.Skip(2).First());
            Assert.Equal(-5, cpu.Operations.Skip(2).First().Value);
            Assert.Equal(5, cpu.TicksRemaining);
        }

        [Fact]
        public void TickWithNoOperationTest()
        {
            var cpu = new Processor();
            var instr = new Operation(OperationType.NoOperation);
            cpu.AddOperation(instr);

            Assert.Equal(instr, cpu.Operations.First());
            Assert.Equal(1, cpu.SpriteHorizontalPosition);
            Assert.Equal(1, cpu.TicksRemaining);

            cpu.Tick();
            Assert.Equal(instr, cpu.PreviousOperation);
            Assert.Null(cpu.CurrentOperation);
            Assert.Equal(1, cpu.SpriteHorizontalPosition);
            Assert.Equal(2, cpu.TicksCount);
            Assert.Equal(0, cpu.TicksRemaining);
        }

        [Fact]
        public void TickWithAddOperationTest()
        {
            var cpu = new Processor();
            var instr = new Operation(OperationType.Add, 3);
            cpu.AddOperation(instr);
            Assert.Equal(2, cpu.TicksRemaining);

            cpu.Tick();
            Assert.Equal(instr, cpu.CurrentOperation);
            Assert.Empty(cpu.Operations);
            Assert.Equal(1, cpu.TicksRemaining);

            cpu.Tick();
            Assert.Null(cpu.CurrentOperation);
            Assert.Equal(instr, cpu.PreviousOperation);
            Assert.Equal(4, cpu.SpriteHorizontalPosition);
            Assert.Equal(3, cpu.TicksCount);
            Assert.Equal(0, cpu.TicksRemaining);
        }

        [Fact]
        public void TickWithNoOpAndAddOpTest()
        {
            var cpu = new Processor();
            cpu.AddOperation(new Operation(OperationType.NoOperation));
            cpu.AddOperation(new Operation(OperationType.Add, 3));
            cpu.AddOperation(new Operation(OperationType.Add, -5));

            cpu.Tick();
            cpu.Tick();
            cpu.Tick();
            cpu.Tick();
            cpu.Tick();

            Assert.Null(cpu.CurrentOperation);
            Assert.Equal(-1, cpu.SpriteHorizontalPosition);
            Assert.Equal(6, cpu.TicksCount);
            Assert.Equal(0, cpu.TicksRemaining);
        }

        [Fact]
        public void SignalStrengthTest()
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

            var cpu = new Processor();
            cpu.AddInstrunctions(operations);

            cpu.Tick(19);
            Assert.Equal(420, cpu.SignalStrength.First());

            cpu.Tick(40);
            Assert.Equal(1140, cpu.SignalStrength.Second());

            cpu.Tick(40);
            Assert.Equal(1800, cpu.SignalStrength.Skip(2).First());

            cpu.Tick(40);
            Assert.Equal(2940, cpu.SignalStrength.Skip(3).First());

            cpu.Tick(40);
            Assert.Equal(2880, cpu.SignalStrength.Skip(4).First());

            cpu.Tick(40);
            Assert.Equal(3960, cpu.SignalStrength.Skip(5).First());

            Assert.Equal(13140, cpu.SignalStrength.Sum());
        }
    }
}
