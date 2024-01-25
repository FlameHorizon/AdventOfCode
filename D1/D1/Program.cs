var input = File.ReadLines("input.txt");
var result = input
    .Select(x => Machine.Calibrate(x))
    .Select(x => x.First * 10 + x.Last)
    .Sum();

Console.WriteLine(result);

public class Machine
{
    public static (int First, int Last) Calibrate(string line)
    {
        // Normalize line
        line = line.Replace("one", "o1e").Replace("two", "t2o")
            .Replace("three", "3").Replace("four", "4")
            .Replace("five", "5").Replace("six", "6")
            .Replace("seven", "7").Replace("eight", "e8t")
            .Replace("nine", "9");
        
        return (line.First(x => char.IsDigit(x)) - '0', 
            line.Last((x => char.IsDigit(x))) - '0');
    }
}