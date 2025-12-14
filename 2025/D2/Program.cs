// See https://aka.ms/new-console-template for more information
const string rangeSeparator = ",";
string[] lines = File.ReadAllText("input.txt").Split(rangeSeparator);
List<IdRange> idRanges = Parser.Parse(lines);

long sum = 0;
foreach (IdRange item in idRanges)
{
    sum += item.InvalidIds().Sum();
}

Console.WriteLine($"Sum: {sum}");

public static class Parser
{
    private const string _idSeparator = "-";

    public static List<IdRange> Parse(string[] lines)
    {
        List<IdRange> result = [];
        foreach (string line in lines)
        {
            IdRange item = Parse(line);
            result.Add(item);
        }

        return result;
    }

    /// <summary>
    /// Parses a single line which should contain single range.
    /// </summary>
    public static IdRange Parse(string line)
    {
        string[] split = line.Split(_idSeparator);
        Console.WriteLine($"Start: {split[0]}");
        Console.WriteLine($"End: {split[1]}");
        return new IdRange()
        {
            Start = Convert.ToInt64(split[0]),
            End = Convert.ToInt64(split[1])
        };
    }
}

public class IdRange
{
    public long Start;
    public long End;

    /// <summary>
    /// Returns number of invalid ids within a range.
    /// Invalid id is that which is a palindrome.
    /// </summary>
    public int CountOfInvalidIds()
    {
        return InvalidIds().Count;
    }

    public List<long> InvalidIds()
    {
        List<long> result = [];
        for (long i = Start; i <= End; i++)
        {
            string number = i.ToString();
            if (number.Length % 2 == 0)
            {
                int middle = number.Length / 2;
                string left = number[0..middle];
                string right = number[middle..];
                if (left == right)
                {
                    result.Add(i);
                }
            }
        }

        return result;
    }
}
