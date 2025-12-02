namespace AdventOfCode.D1;

public static class Program
{
    public static void Main()
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
        string separator = Environment.NewLine + Environment.NewLine;
        string path = Path.Combine("data", "input.txt");
        string data = File.ReadAllText(path);

        return data
            .Split(separator)
            .Select(x => x.Split(Environment.NewLine).Select(int.Parse).Sum())
            .Max();
    }

    private static int GetSumOfTopThreeTotalCalories()
    {
        string separator = Environment.NewLine + Environment.NewLine;
        string data = File.ReadAllText(@"data/input.txt");

        return data
            .Split(separator)
            .Select(x => x.Split(Environment.NewLine).Select(int.Parse).Sum())
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