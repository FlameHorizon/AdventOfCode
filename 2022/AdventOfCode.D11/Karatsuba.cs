using System.Numerics;

namespace AdventOfCode.D11
{
    public static class Karatsuba
    {
        public static string Multiply(string first, string second)
        {
            if (first.Length == 1 || second.Length == 1)
                return (BigInteger.Parse(first) * BigInteger.Parse(second)).ToString();
            int cutPos = getCutPosition(first, second);
            string a = getFirstPart(first, cutPos);
            string b = getSecondPart(first, cutPos);
            string c = getFirstPart(second, cutPos);
            string d = getSecondPart(second, cutPos);
            string ac = Multiply(a, c);
            string bd = Multiply(b, d);
            string ab_cd = Multiply(stringAddtion(a, b), stringAddtion(c, d));
            return calculateResult(ac, bd, ab_cd, b.Length + d.Length);
        }

        static string calculateResult(string ac, string bd, string ab_cd, int padding)
        {
            string term0 = stringSubtraction(stringSubtraction(ab_cd, ac), bd);
            string term1 = term0.PadRight(term0.Length + padding / 2, '0');
            string term2 = ac.PadRight(ac.Length + padding, '0');
            return stringAddtion(stringAddtion(term1, term2), bd);
        }

        static string getFirstPart(string str, int cutPos)
        {
            return str.Remove(str.Length - cutPos);
        }

        static string getSecondPart(string str, int cutPos)
        {
            return str.Substring(str.Length - cutPos);
        }

        static int getCutPosition(string first, string second)
        {
            int min = Math.Min(first.Length, second.Length);
            if (min == 1)
                return 1;
            if (min % 2 == 0)
                return min / 2;
            return min / 2 + 1;
        }

        static string stringAddtion(string a, string b)
        {
            string result = "";
            //a is always the smallest in length
            if (a.Length > b.Length)
            {
                swap(ref a, ref b);
            }
            a = a.PadLeft(b.Length, '0');
            int length = a.Length;
            int carry = 0, res;
            for (int i = length - 1; i >= 0; i--)
            {
                int num1 = int.Parse(a.Substring(i, 1));
                int num2 = int.Parse(b.Substring(i, 1));
                res = (num1 + num2 + carry) % 10;
                carry = (num1 + num2 + carry) / 10;
                result = result.Insert(0, res.ToString());
            }
            if (carry != 0)
                result = result.Insert(0, carry.ToString());
            return sanitizeResult(result);
        }

        static string stringSubtraction(string a, string b)
        {
            bool resultNegative = false;
            string result = "";
            //a should be the larger number
            if (stringIsSmaller(a, b))
            {
                swap(ref a, ref b);
                resultNegative = true;
            }
            b = b.PadLeft(a.Length, '0');
            int length = a.Length;
            int carry = 0, res;
            for (int i = length - 1; i >= 0; i--)
            {
                bool nextCarry = false;
                int num1 = int.Parse(a.Substring(i, 1));
                int num2 = int.Parse(b.Substring(i, 1));
                if (num1 - carry < num2)
                {
                    num1 = num1 + 10;
                    nextCarry = true;
                }
                res = (num1 - num2 - carry);
                result = result.Insert(0, res.ToString());
                if (nextCarry)
                    carry = 1;
                else
                    carry = 0;
            }
            result = sanitizeResult(result);
            if (resultNegative)
                return result.Insert(0, "-");
            return result;
        }

        static bool stringIsSmaller(string a, string b)
        {
            if (a.Length < b.Length)
                return true;
            if (a.Length > b.Length)
                return false;
            char[] arrayA = a.ToCharArray();
            char[] arrayB = b.ToCharArray();
            for (int i = 0; i < arrayA.Length; i++)
            {
                if (arrayA[i] < arrayB[i])
                    return true;
                if (arrayA[i] > arrayB[i])
                    return false;
            }
            return false;
        }

        static void swap(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }

        static string sanitizeResult(string result)
        {
            result = result.TrimStart(new char[] { '0' });
            if (result.Length == 0)
                result = "0";
            return result;
        }
    }
}
