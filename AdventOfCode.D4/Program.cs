namespace AdventOfCode.D4
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
            string data = File.ReadAllText(@"data\input.txt");
            int count = data.Split("\r\n").Count(ln => ElfPairHasInside(ln));

            Console.WriteLine($"Count of overlapping assignments {count}.");
        }

        private class Assignment
        {
            public int Start { get; set; }
            public int End { get; set; }

            public Assignment(string value)
            {
                Start = int.Parse(value.Split("-")[0]);
                End = int.Parse(value.Split("-")[1]);
            }

            public bool HasInside(Assignment section)
            {
                return HasInside(section, Start) && HasInside(section, End);
            }

            private static bool HasInside(Assignment section, int value)
            {
                return value >= section.Start && value <= section.End;
            }

            public bool IntersectsWith(Assignment test)
            {
                return HasInside(this, test.Start)
                    || HasInside(this, test.End)
                    || (test.Start < Start && test.End > End);
            }
        }

        private static bool ElfPairHasInside(string value)
        {
            string[] assignments = value.Split(",");
            var first = new Assignment(assignments[0]);
            var second = new Assignment(assignments[1]);

            return first.HasInside(second) || second.HasInside(first);
        }

        private static bool ElfPairIntersectsWith(string value)
        {
            string[] assignments = value.Split(",");
            var first = new Assignment(assignments[0]);
            var second = new Assignment(assignments[1]);

            return first.IntersectsWith(second);
        }

        private static void Part2()
        {
            string data = File.ReadAllText(@"data\input.txt");
            int count = data.Split("\r\n").Count(ln => ElfPairIntersectsWith(ln));

            Console.WriteLine($"Count of intersecting assignments {count}");
        }
    }
}