namespace AdventOfCode.D10
{
    public static class IEnumerableExtensions
    {
        public static TSource Second<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(1).First();
        }
    }
}
