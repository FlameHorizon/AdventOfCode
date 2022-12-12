using System.Numerics;
using static AdventOfCode.D11.Program;

namespace AdventOfCode.D11.Tests
{
    public class MokeyTests
    {
        [Fact]
        public void InitTest()
        {
            var input =
@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3";

            var monkey = new Monkey(input);

            Assert.Equal(0u, monkey.Id);
            Assert.Equal(79u, monkey.Items.First());
            Assert.Equal(98u, monkey.Items.Skip(1).First());
            Assert.Equal(1501u, monkey.Operation(monkey.Items.First()));
            Assert.Equal(23, monkey.Test);
            Assert.Equal(2, monkey.PassIfDivisable);
            Assert.Equal(3, monkey.PassIfNotDivisable);
        }

        [Fact]
        public void InitWithAdditionTest()
        {
            var input =
@"Monkey 0:
  Starting items: 1
  Operation: new = old + 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3";

            var monkey = new Monkey(input);
            Assert.Equal(20u, monkey.Operation(monkey.Items.First()));
        }

        [Fact]
        public void InitWithOldTest()
        {
            var input =
@"Monkey 0:
  Starting items: 3
  Operation: new = old + old
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3";

            var monkey = new Monkey(input);
            Assert.Equal(6u, monkey.Operation(monkey.Items.First()));

            var input2 =
@"Monkey 1:
  Starting items: 3
  Operation: new = old * old
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3";

            var monkey2 = new Monkey(input2);
            Assert.Equal(9u, monkey2.Operation(monkey2.Items.First()));
        }

        [Fact]
        public void InspectTest()
        {
            var input =
@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3";

            var monkey = new Monkey(input);

            Assert.Equal(3, monkey.Inspect().First());
            Assert.Equal(3, monkey.Inspect()[1]);
        }

        [Fact]
        public void ThrowTest()
        {
            var input =
@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3";

            var monkeys = Enumerable
                .Range(1, 2)
                .Select(_ => new Monkey(input));

            Monkey first = monkeys.First();
            Monkey second = monkeys.Skip(1).First();

            first.Throw(second);

            Assert.Equal(new List<BigInteger> { 98 }, first.Items);
            Assert.Equal(new List<BigInteger> { 79, 98, 500 }, second.Items);
            Assert.Equal(1u, first.InspectedItemsCount);
        }
    }
}