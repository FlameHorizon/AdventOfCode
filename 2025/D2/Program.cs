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

            if (number.Length == 1)
            {
                Console.WriteLine("Number contians only one digit.");
                continue;
            }

            // Case where all digits are the same.
            if (number.Distinct().Count() == 1)
            {
                Console.WriteLine("All digits are the same in {0}", number);
                result.Add(i);
                continue;
            }

            if (number.Length % 2 == 0)
            {
                int middle = number.Length / 2;
                string left = number[0..middle];
                string right = number[middle..];
                if (left == right)
                {
                    Console.WriteLine("Found palindrome, left-right {0} - {1} in {2}", left, right, number);
                    result.Add(i);
                    continue;
                }
            }

            // Depending on the size of the number
            // repeating pattern will have either 2 or 3 characters.
            // If size is even, pattern must repeating pattern
            // must have length of two. Otherwise, pattern must have 
            // length of three.
            List<int> possiblePhraseLength = [];

            if (number.Length % 6 == 0)
            {
                // Length of pattern can be both 3 or 2.
                // Console.WriteLine("For number '{0}', selected 3 and 2 length.", number);
                possiblePhraseLength.Add(3);
                possiblePhraseLength.Add(2);
            }
            else if (number.Length % 3 == 0)
            {
                // Console.WriteLine("For number '{0}', selected 3 length.", number);
                possiblePhraseLength.Add(3);
            }
            else
            {
                // Console.WriteLine("For number '{0}', selected 2 length.", number);
                possiblePhraseLength.Add(2);
            }

            foreach (int length in possiblePhraseLength)
            {
                // Console.WriteLine("Using length: {0}", length);
                string phrase = number[0..length];
                // Console.WriteLine("Got phrase: {0}", phrase);
                int occurenceCount = 0;
                int index = 0;
                while ((index = number.IndexOf(phrase, index)) != -1)
                {
                    // Console.WriteLine("At index {0}", index);
                    index += length;
                    occurenceCount++;
                }

                if (occurenceCount > 1 &&
                    occurenceCount * length == number.Length)
                {
                    Console.WriteLine("Found repeating phrase '{0}' in {1}", phrase, number);
                    result.Add(i);
                    continue;
                }
            }
        }

        return result;
    }
}
