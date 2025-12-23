public static class BankAnalyzer
{
    public static int FindMaxJoltage(string schema)
    {
        int result = 0;
        for (int i = 0; i < schema.Length - 1; i++)
        {
            for (int j = i + 1; j < schema.Length; j++)
            {
                char a = schema[i];
                char b = schema[j];
                string ab = new string(new char[] { a, b });
                int num = Convert.ToInt32(ab);

                if (num > result)
                {
                    result = num;
                }
            }
        }
        return result;
    }

    public static long FindMaxJoltage12Batteries(string schema)
    {
        if (schema.Length == 12)
        {
            return Convert.ToInt64(schema);
        }

        // First first highest digit, search space has to be limited
        // to offest.
        int offset = 12;
        int start = 0;
        int end = schema.Length - offset + 1;
        long max = 0;
        string buffer = "";
        int indexOfMax = 0;
        while (buffer.Length < 12)
        {
            for (int i = start; i < end; i++)
            {
                Console.WriteLine("Current index: {0}, end: {1}", i, end);
                long curr = Convert.ToInt64(schema[i].ToString());
                if (curr > max)
                {
                    indexOfMax = i;
                    max = curr;
                }
            }

            // Update scope of search such that next search
            // moves to right by, the found index.
            buffer += max.ToString();
            Console.WriteLine("Found max in range: {0}:{1}. Current buffer: {2}", start, end, buffer);
            start = indexOfMax + 1;
            offset -= 1;
            end = schema.Length - offset + 1;
            max = 0;
        }
        return Convert.ToInt64(buffer);

    }
}
