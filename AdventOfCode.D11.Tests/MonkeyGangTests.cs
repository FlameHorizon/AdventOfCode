using System.Numerics;
using static AdventOfCode.D11.Program;

namespace AdventOfCode.D11.Tests
{
    public class MonkeyGangTests
    {
        [Fact]
        public void MonkeyGangInitTest()
        {
            var gang = new MonkeyGang();
            var operation = new Func<BigInteger, BigInteger>(x => x + 1);

            gang.AddMonkey(new Monkey(id: 1,
                                      new Queue<BigInteger>(),
                                      operation,
                                      test: 1,
                                      passIfDivisable: 1,
                                      passIfNotDivisable: 2));

            Assert.Contains(gang.Monkeys, m => m.Id == 1);
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
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

            var gang = new MonkeyGang(input);

            Assert.Equal(4, gang.Monkeys.Count);

            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();

            // After round 1.
            Assert.Equal(new List<BigInteger> { 20, 23, 27, 26 }, gang.Monkeys.First().Items);
            Assert.Equal(new List<BigInteger> { 2080, 25, 167, 207, 401, 1046 }, gang.Monkeys.Skip(1).First().Items);
            Assert.Empty(gang.Monkeys.Skip(2).First().Items);
            Assert.Empty(gang.Monkeys.Skip(3).First().Items);

            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();

            // After round 2.
            Assert.Equal(5, gang.Monkeys.First().Items.Count);
            Assert.Equal(5, gang.Monkeys.Skip(1).First().Items.Count);
            Assert.Empty(gang.Monkeys.Skip(2).First().Items);
            Assert.Empty(gang.Monkeys.Skip(3).First().Items);

            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();

            // After round 3.
            Assert.Equal(5, gang.Monkeys.First().Items.Count);
            Assert.Equal(5, gang.Monkeys.Skip(1).First().Items.Count);
            Assert.Empty(gang.Monkeys.Skip(2).First().Items);
            Assert.Empty(gang.Monkeys.Skip(3).First().Items);

            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();
            gang.NextMonkeyTurn();

            // After round 4.
            Assert.Equal(new List<BigInteger> { 491, 9, 52, 97, 248, 34 }, gang.Monkeys.First().Items);
            Assert.Equal(new List<BigInteger> { 39, 45, 43, 258 }, gang.Monkeys.Skip(1).First().Items);
            Assert.Empty(gang.Monkeys.Skip(2).First().Items);
            Assert.Empty(gang.Monkeys.Skip(3).First().Items);
        }

        [Fact]
        public void RoundTest()
        {
            var input =
@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

            var gang = new MonkeyGang(input);

            Assert.Equal(4, gang.Monkeys.Count);

            gang.Round();

            // After round 1.
            Assert.Equal(new List<BigInteger> { 20, 23, 27, 26 }, gang.Monkeys.First().Items);
            Assert.Equal(new List<BigInteger> { 2080, 25, 167, 207, 401, 1046 }, gang.Monkeys.Skip(1).First().Items);
            Assert.Empty(gang.Monkeys.Skip(2).First().Items);
            Assert.Empty(gang.Monkeys.Skip(3).First().Items);

            gang.Round();

            // After round 2.
            Assert.Equal(5, gang.Monkeys.First().Items.Count);
            Assert.Equal(5, gang.Monkeys.Skip(1).First().Items.Count);
            Assert.Empty(gang.Monkeys.Skip(2).First().Items);
            Assert.Empty(gang.Monkeys.Skip(3).First().Items);

            gang.Round();

            // After round 3.
            Assert.Equal(5, gang.Monkeys.First().Items.Count);
            Assert.Equal(5, gang.Monkeys.Skip(1).First().Items.Count);
            Assert.Empty(gang.Monkeys.Skip(2).First().Items);
            Assert.Empty(gang.Monkeys.Skip(3).First().Items);

            gang.Round();

            // After round 4.
            Assert.Equal(new List<BigInteger> { 491, 9, 52, 97, 248, 34 }, gang.Monkeys.First().Items);
            Assert.Equal(new List<BigInteger> { 39, 45, 43, 258 }, gang.Monkeys.Skip(1).First().Items);
            Assert.Empty(gang.Monkeys.Skip(2).First().Items);
            Assert.Empty(gang.Monkeys.Skip(3).First().Items);
        }

        [Fact]
        public void RoundComplexTest()
        {
            var input =
@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

            var gang = new MonkeyGang(input);

            for (BigInteger i = 0; i < 20; i++)
            {
                gang.Round();
            }

            Assert.Equal(101u, gang.Monkeys.First().InspectedItemsCount);
            Assert.Equal(95u, gang.Monkeys.Skip(1).First().InspectedItemsCount);
            Assert.Equal(7u, gang.Monkeys.Skip(2).First().InspectedItemsCount);
            Assert.Equal(105u, gang.Monkeys.Skip(3).First().InspectedItemsCount);
        }

        [Fact]
        public void ThrowAdjustedTest()
        {
            var input =
@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

            var gang = new MonkeyGang(input);
            //gang.RoundAdjusted();

            //Assert.Equal(2, gang.Monkeys.First().InspectedItemsCount);
            //Assert.Equal(4, gang.Monkeys.Skip(1).First().InspectedItemsCount);
            //Assert.Equal(3, gang.Monkeys.Skip(2).First().InspectedItemsCount);
            //Assert.Equal(6, gang.Monkeys.Skip(3).First().InspectedItemsCount);

            //for (BigInteger i = 0; i < 19; i++)
            //{
            //    gang.RoundAdjusted();
            //}

            //Assert.Equal(99, gang.Monkeys.First().InspectedItemsCount);
            //Assert.Equal(97, gang.Monkeys.Skip(1).First().InspectedItemsCount);
            //Assert.Equal(8, gang.Monkeys.Skip(2).First().InspectedItemsCount);
            //Assert.Equal(103, gang.Monkeys.Skip(3).First().InspectedItemsCount);

            for (int i = 0; i < 10000; i++)
            {
                gang.RoundAdjusted();
            }

            Assert.Equal(52166, gang.Monkeys.First().InspectedItemsCount);
            Assert.Equal(47830, gang.Monkeys.Skip(1).First().InspectedItemsCount);
            Assert.Equal(1938, gang.Monkeys.Skip(2).First().InspectedItemsCount);
            Assert.Equal(52013, gang.Monkeys.Skip(3).First().InspectedItemsCount);

            var top2 = gang.Monkeys.OrderByDescending(m => m.InspectedItemsCount).Take(2);
            Assert.Equal(2713310158, top2.First().InspectedItemsCount * top2.Skip(1).First().InspectedItemsCount);

        }
    }
}
