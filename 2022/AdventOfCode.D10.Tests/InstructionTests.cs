using static AdventOfCode.D10.Program;

namespace AdventOfCode.D10.Tests
{
    public class OperationTests
    {
        [Fact]
        public void InitNoOpOperationTest()
        {
            string op = "noop";

            var instr = new Operation(op);

            Assert.Equal(OperationType.NoOperation, instr.Type);
            Assert.Equal(1, instr.DurationInCycles);
            Assert.Equal(0, instr.Value);
            Assert.Equal(1, instr.TicksLeft);
        }

        [Fact]
        public void InitAddOperationTest()
        {
            string op = "addx 3";

            var instr = new Operation(op);

            Assert.Equal(OperationType.Add, instr.Type);
            Assert.Equal(2, instr.DurationInCycles);
            Assert.Equal(3, instr.Value);
            Assert.Equal(2, instr.TicksLeft);
        }
    }
}