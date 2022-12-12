namespace AdventOfCode.D11.Others
{
    using System.Numerics;

    class Monkey
    {
        public string Operation { get; set; }
        public string Test { get; set; }
        public int IfTrueMonkey { get; set; }
        public int IfFalseMonkey { get; set; }
        public List<BigInteger> Items { get; set; }

        public BigInteger InspectCount { get; set; } = 0;

        public Monkey(string operation, string test, int ifTrueMonkey, int ifFalseMonkey, List<int> items)
        {
            Operation = operation;
            Test = test;
            IfTrueMonkey = ifTrueMonkey;
            IfFalseMonkey = ifFalseMonkey;
            Items = items.Select(x => (BigInteger)x).ToList();
        }

        private BigInteger compute(string expression)
        {
            var parts = expression.Split(" ");

            switch (parts[1])
            {
                case "*":
                    return BigInteger.Parse(parts[0]) * BigInteger.Parse(parts[2]);
                case "+":
                    return BigInteger.Parse(parts[0]) + BigInteger.Parse(parts[2]);
                case "%":
                    return BigInteger.Parse(parts[0]) % BigInteger.Parse(parts[2]);
            }

            throw new Exception($"Unknown operation {parts[1]}");
        }

        public void DoRound(List<Monkey> monkeys)
        {
            foreach (var item in Items)
            {
                InspectCount++;

                var newWorry = compute(Operation.Replace("old", item.ToString()));
                int aggregate = monkeys.Aggregate(1, (i, monkey) => i * int.Parse(monkey.Test));
                newWorry %= aggregate;

                var targetMonkey = compute($"{newWorry} % {Test}") == 0 ? IfTrueMonkey : IfFalseMonkey;
                monkeys[targetMonkey].Items.Add(newWorry);
            }

            Items.Clear();
        }
    }

    class Program
    {
        static void Main()
        {
            var monkeys = new List<Monkey>();

            File.ReadAllLines("input.txt")
                .Chunk(7).ToList()
                .ForEach(line =>
                {
                    var startingItems = line[1].Trim().Replace("Starting items: ", "").Split(", ").Select(int.Parse)
                        .ToList();
                    var operation = line[2].Trim().Replace("Operation: new = ", "");
                    var test = line[3].Trim().Replace("Test: divisible by ", "");
                    var ifTrue = int.Parse(line[4].Trim().Replace("If true: throw to monkey ", ""));
                    var ifFalse = int.Parse(line[5].Trim().Replace("If false: throw to monkey ", ""));

                    var monkey = new Monkey(operation, test, ifTrue, ifFalse, startingItems);
                    monkeys.Add(monkey);
                });

            for (var round = 1; round <= 10000; round++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.DoRound(monkeys);
                }
            }

            var score = monkeys.OrderBy(e => e.InspectCount).TakeLast(2).Aggregate((BigInteger)1, (a, b) => a * b.InspectCount);

            Console.WriteLine(score);
        }
    }
}