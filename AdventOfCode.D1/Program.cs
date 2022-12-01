using System.Linq;
using System.Threading;

namespace AdventOfCode.D1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Part1();
            Part2();

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void Part1()
        {
            Console.WriteLine(
                "Find the Elf carrying the most Calories. "
                + "How many total Calories is that Elf carrying?");

            int maxTotalCalories = GetMaxCaloriesOnElf();
            Console.WriteLine(maxTotalCalories);
        }

        private static int GetMaxCaloriesOnElf()
        {
            const string separator = "\r\n\r\n";
            var data = File.ReadAllText(@"data\input.txt");

            return data
                .Split(separator)
                .Select(x => x.Split("\r\n").Select(int.Parse).Sum())
                .Max();
        }

        private static int GetSumOfTopThreeTotalCalories()
        {
            const string separator = "\r\n\r\n";
            var data = File.ReadAllText(@"data\input.txt");

            return data
                .Split(separator)
                .Select(x => x.Split("\r\n").Select(int.Parse).Sum())
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();
        }

        private static void Part2()
        {
            Console.WriteLine(
                "Find the top three Elves carrying the most Calories. "
                + "How many Calories are those Elves carrying in total?");

            int sumOfTopThree = GetSumOfTopThreeTotalCalories();
            Console.WriteLine(sumOfTopThree);
        }
    }
}