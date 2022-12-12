using System.Numerics;

namespace AdventOfCode.D11
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
            Console.WriteLine("What is the level of monkey business "
                + "after 20 rounds of stuff-slinging simian shenanigans?");

            var input = File.ReadAllText("input.txt");
            var gang = new MonkeyGang(input);

            for (BigInteger i = 0; i < 20; i++)
            {
                gang.Round();
            }

            var ordered = gang.Monkeys.OrderByDescending(m => m.InspectedItemsCount);
            var first = ordered.First();
            var second = ordered.Skip(1).First();

            var result = first.InspectedItemsCount * second.InspectedItemsCount;
            Console.WriteLine(result);
        }

        public class Monkey
        {
            public BigInteger Id { get; private set; }
            public Queue<BigInteger> Items { get; init; }
            public Func<BigInteger, BigInteger> Operation { get; init; }
            public int Test { get; init; }
            public int PassIfDivisable { get; init; }
            public int PassIfNotDivisable { get; init; }
            public BigInteger InspectedItemsCount { get; private set; } = 0;

            private string _sign = string.Empty;
            private string _value = string.Empty;

            public Monkey(BigInteger id,
                          Queue<BigInteger> items,
                          Func<BigInteger, BigInteger> operation,
                          int test,
                          int passIfDivisable,
                          int passIfNotDivisable)
            {
                Id = id;
                Items = items ?? throw new ArgumentNullException(nameof(items));
                Operation = operation ?? throw new ArgumentNullException(nameof(operation));
                Test = test;
                PassIfDivisable = passIfDivisable;
                PassIfNotDivisable = passIfNotDivisable;
            }

            public Monkey(string input)
            {
                string[] split = input.Split(Environment.NewLine);

                var rawId = split[0]
                    .Split(" ")[1]
                    .Replace(":", string.Empty);

                Id = BigInteger.Parse(rawId);

                var rawItems = split[1]
                    .Replace("  Starting items: ", string.Empty)
                    .Split(",");

                Items = new Queue<BigInteger>(rawItems.Select(i => BigInteger.Parse(i)));

                _sign = split[2].Split(" ")[6];
                _value = split[2].Split(" ")[7];

                if (_sign == "*" && _value != "old")
                {
                    Operation = (BigInteger x) => x * BigInteger.Parse(_value);
                    //Operation = (BigInteger x) => BigInteger.Multiply(x, BigInteger.Parse(_value));
                    //Operation = (BigInteger x) => PowerOptimised(x, 2);
                    //Operation = (BigInteger x) => BigInteger.Parse(Karatsuba.Multiply(x.ToString(), _value));

                }
                else if (_sign == "+" && _value != "old")
                {
                    Operation = (BigInteger x) => x + BigInteger.Parse(_value);
                }
                else if (_sign == "+" && _value == "old")
                {
                    Operation = (BigInteger x) => x + x;
                }
                else
                {
                    Operation = (BigInteger x) => x * x;
                    //Operation = (BigInteger x) => x << 1;
                    //Operation = (BigInteger x) => PowerOptimised(x, 2);
                    //Operation = (BigInteger x) => BigInteger.Parse(Karatsuba.Multiply(x.ToString(), x.ToString()));

                }

                Test = int.Parse(split[3].Split(" ").Last());
                PassIfDivisable = int.Parse(split[4].Split(" ").Last());
                PassIfNotDivisable = int.Parse(split[5].Split(" ").Last());
            }

            public List<int> Inspect()
            {
                return Items.Select(
                    i =>
                    {
                        BigInteger v = Operation(i) / 3;
                        return v % Test == 0 ? PassIfDivisable
                                             : PassIfNotDivisable;
                    })
                    .ToList();
            }

            public List<(int, BigInteger)> InspectAdjusted(List<Monkey> monkeys)
            {
                var output = new List<(int, BigInteger)>();

                foreach (var item in Items)
                {
                    var newWorry = Process(item);
                    int aggregate = monkeys.Aggregate(1, (i, monkey) => i * monkey.Test);
                    newWorry %= aggregate;

                    BigInteger worryLevel = newWorry % Test;
                    var targetMonkey = worryLevel == 0
                                        ? PassIfDivisable
                                        : PassIfNotDivisable;

                    output.Add((targetMonkey, newWorry));
                }

                return output;


                //return Items.Select(
                //    i =>
                //    {
                //        //BigInteger v = Operation(i);
                //        BigInteger v = Process(i);
                //        v %= monkeys.Aggregate(1, (i, monkey) => i * monkey.Test);

                //        BigInteger worryLevel = v % Test;
                //        return worryLevel == 0 ? PassIfDivisable
                //                             : PassIfNotDivisable;
                //    })
                //    .ToList();
            }

            private BigInteger Process(BigInteger i)
            {
                if (_sign == "*" && _value != "old")
                {
                    return (BigInteger)i * BigInteger.Parse(_value);
                }
                else if (_sign == "+" && _value != "old")
                {
                    return i + BigInteger.Parse(_value);
                }
                else if (_sign == "+" && _value == "old")
                {
                    return i + i;
                }
                else
                {
                    return (BigInteger)i * (BigInteger)i;
                }
            }

            public void Throw(Monkey target)
            {
                var item = Items.Dequeue();
                item = Operation(item) / 3;
                target.Items.Enqueue(item);

                InspectedItemsCount++;
            }

            public void ThrowAdjusted(Monkey target, BigInteger value)
            {
                _ = Items.Dequeue();
                // item = Operation(item);
                target.Items.Enqueue(value);

                InspectedItemsCount++;
            }
        }

        public class MonkeyGang
        {
            private readonly List<Monkey> _monkeys = new();
            private BigInteger _currentMonkeyIndex = 0;
            private BigInteger _maxMonkeyIndex = -1;
            public IReadOnlyList<Monkey> Monkeys => _monkeys;

            public MonkeyGang()
            {

            }

            public MonkeyGang(string raw)
            {
                foreach (string section in raw.Split("\r\n\r\n"))
                {
                    AddMonkey(new Monkey(section));
                }
                _maxMonkeyIndex = _monkeys.Count - 1;
            }

            public void AddMonkey(Monkey monkey)
            {
                _monkeys.Add(monkey);
                _maxMonkeyIndex++;
            }

            public void NextMonkeyTurn()
            {
                if (_currentMonkeyIndex > _maxMonkeyIndex)
                {
                    _currentMonkeyIndex = 0;
                }

                var monkey = _monkeys[(int)_currentMonkeyIndex];
                var throwPlan = monkey.Inspect();

                foreach (BigInteger index in throwPlan)
                {
                    var throwTo = _monkeys[(int)index];
                    monkey.Throw(throwTo);
                }

                _currentMonkeyIndex++;
            }

            public void Round()
            {
                for (int i = 0; i < _monkeys.Count; i++)
                {
                    NextMonkeyTurn();
                }
            }

            public void RoundAdjusted()
            {
                for (int i = 0; i < _monkeys.Count; i++)
                {
                    NextMonkeyTurnAdjusted();
                }
            }

            public void NextMonkeyTurnAdjusted()
            {
                if (_currentMonkeyIndex > _maxMonkeyIndex)
                {
                    _currentMonkeyIndex = 0;
                }

                var monkey = _monkeys[(int)_currentMonkeyIndex];


                var throwPlan = monkey.InspectAdjusted(_monkeys);

                foreach ((int index, BigInteger value) in throwPlan)
                {
                    var throwTo = _monkeys[(int)index];
                    monkey.ThrowAdjusted(throwTo, value);
                }

                _currentMonkeyIndex++;
            }
        }

        private static void Part2()
        {
            Console.WriteLine("Starting again from the initial state in your puzzle input, "
                + "what is the level of monkey business after 10000 rounds?");

            var input = File.ReadAllText("input.txt");
            var gang = new MonkeyGang(input);

            for (BigInteger i = 0; i < 10000; i++)
            {
                gang.RoundAdjusted();
            }

            var ordered = gang.Monkeys.OrderByDescending(m => m.InspectedItemsCount);
            var first = ordered.First();
            var second = ordered.Skip(1).First();

            var result = first.InspectedItemsCount * second.InspectedItemsCount;
            Console.WriteLine(result);
        }
    }
}