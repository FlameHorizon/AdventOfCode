using System.Text;

namespace D4;

public static class RollsLayoutAnalyzer
{
    public static (int, string) AccesableRolls(string rollsLayout)
    {
        // Assumption is each line has the same number of characters.
        string[] rows = rollsLayout.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        int rowCount = rows.Length;
        int columnCount = rows[0].Length;
        int neighbours = 0;
        int result = 0;
        // There is not simple way of modifying individual characters.
        // Have to create array of chars and then covert it back to new string.
        char[] chars = rollsLayout.Replace(Environment.NewLine, "").ToCharArray();

        // Going over each row
        for (int y = 0; y < rowCount; y++)
        {
            Console.WriteLine("Reading line: {0}", rows[y]);
            // Going over each column
            for (int x = 0; x < columnCount; x++)
            {
                Console.WriteLine($"Accessing: row {y}, col {x}");
                char c = rows[y][x];
                if (c == '.')
                {
                    // This is empty space, don't do any calculations
                    continue;
                }

                if (c == '@')
                {
                    // This is roll of paper. Figure out how many rolls of papers
                    // surrounds it.
                    char n;

                    // Check one row above.
                    if (y > 0)
                    {
                        // Top left
                        if (x > 0)
                        {
                            n = rows[y - 1][x - 1];
                            if (n == '@')
                            {
                                neighbours++;
                            }
                        }

                        // Top
                        n = rows[y - 1][x];
                        if (n == '@')
                        {
                            neighbours++;
                        }

                        // Top right
                        if (x + 1 < columnCount)
                        {
                            n = rows[y - 1][x + 1];
                            if (n == '@')
                            {
                                neighbours++;
                            }
                        }
                    }

                    // Check same row.
                    // Left side.
                    if (x > 0)
                    {
                        n = rows[y][x - 1];
                        if (n == '@')
                        {
                            neighbours++;
                        }
                    }

                    // Right side.
                    if (x + 1 < columnCount)
                    {
                        n = rows[y][x + 1];
                        if (n == '@')
                        {
                            neighbours++;
                        }
                    }

                    // Check row below
                    if (y + 1 < rowCount)
                    {
                        // Bottom left
                        if (x > 0)
                        {
                            n = rows[y + 1][x - 1];
                            if (n == '@')
                            {
                                neighbours++;
                            }
                        }

                        // Bottom
                        Console.WriteLine($"Accessing: row {y + 1}, col {x}");
                        n = rows[y + 1][x];
                        if (n == '@')
                        {
                            neighbours++;
                        }

                        // Bottom right
                        if (x + 1 < columnCount)
                        {
                            n = rows[y + 1][x + 1];
                            if (n == '@')
                            {
                                neighbours++;
                            }
                        }
                    }
                }

                if (neighbours < 4)
                {
                    result++;
                    // Find out the index of the character.
                    int index = (y * rowCount) + x;
                    Console.WriteLine("Found accesable roll at row {0}, col {1}", y, x);
                    Console.WriteLine("Replacing character at index: {0}", index);

                    chars[index] = '.';
                }
                neighbours = 0;
            }
        }

        // // Convert chars into single string columnCount long.
        var sb = new StringBuilder();
        List<char> temp = [];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                int index = (i * rowCount) + j;
                char c = chars[index];
                sb.Append(c);
                temp.Add(c);
            }

            sb.Append(Environment.NewLine);
        }

        sb.Remove(sb.Length - 1, 1);

        // FIX: This will have new line at the end.
        return (result, sb.ToString());
    }

    public static int MultipassAccesableRolls(string rollsLayout)
    {
        int result = 0;
        int lastResult = int.MaxValue;
        string currentLayout = rollsLayout;

        while (lastResult > 0)
        {
            (lastResult, currentLayout) = RollsLayoutAnalyzer.AccesableRolls(currentLayout);
            result += lastResult;
        }

        return result;
    }
}

