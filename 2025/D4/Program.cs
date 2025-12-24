using D4;

string input = File.ReadAllText("input.txt");

int part1 = RollsLayoutAnalyzer.AccesableRolls(input).Item1;
Console.WriteLine($"Solution to Part 1: {part1}");

int part2 = RollsLayoutAnalyzer.MultipassAccesableRolls(input);
Console.WriteLine($"Solution to Part 2: {part2}");

