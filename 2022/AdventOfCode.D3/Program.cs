namespace AdventOfCode.D3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void Part1()
        {
            Console.WriteLine("Find the item type that appears in both compartments of "
                + "each rucksack. What is the sum of the priorities of those item types?");

            int result = SumOfPriorities();
            Console.WriteLine(result);
        }

        private static int SumOfPriorities()
        {
            int result = 0;
            foreach (string line in File.ReadLines(@"data\input.txt"))
            {
                // Find out where is the middle and split string in the middle
                int halfSize = line.Length / 2;

                string firstHalf = line[0..halfSize];
                string secondHalf = line[halfSize..];

                // Find out the duplicate
                char duplicate = firstHalf.Intersect(secondHalf).SingleOrDefault();

                // Calculate the priority of the item and add it
                result += CalculatePriority(duplicate);
            }

            return result;
        }

        private static int CalculatePriority(char c)
        {
            return char.IsLower(c)
                ? c - 97 + 1
                : c - 65 + 26 + 1;
        }

        private static void Part2()
        {
            Console.WriteLine("Find the item type that corresponds to the badges of each three-Elf group. "
                + "What is the sum of the priorities of those item types?");

            int result = SumOfPrioritiesOfBadges();
            Console.WriteLine(result);
        }

        private static int SumOfPrioritiesOfBadges()
        {
            int result = 0;
            string[] data = File.ReadAllLines(@"data\input.txt");
            for (int i = 0; i < data.Length; i += 3)
            {
                IEnumerable<char> intersectOne = data[i].Intersect(data[i + 1]);
                IEnumerable<char> intersectTwo = intersectOne.Intersect(data[i + 2]);

                result += CalculatePriority(intersectTwo.SingleOrDefault());
            }

            return result;
        }
    }
}