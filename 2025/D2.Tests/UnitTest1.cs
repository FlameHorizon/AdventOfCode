using D2;
using Xunit;

namespace D2.Tests;

public class UnitTest1
{
    [Fact]
    public void ParseSingleLine()
    {
      const string line = "11-22";
      IdRange result = Parser.Parse(line);
      Assert.Equal((11, 22), (result.Start, result.End));
    }

    [Theory]
    [InlineData("11-22", 2)]
    [InlineData("95-115", 2)]
    [InlineData("1188511880-1188511890", 1)]
    [InlineData("222220-222224", 1)]
    [InlineData("1698522-1698528", 0)]
    public void CountsInvalidIds(string line, int count) {
      IdRange result = Parser.Parse(line);
      Assert.Equal(count, result.CountOfInvalidIds());
    }

    [Fact]
    public void ReturnsInvalidIds() {
      List<long> result = Parser.Parse("95-115").InvalidIds();
      Assert.Equal((99, 111), (result.First(), result.Last()));

      result = Parser.Parse("565653-565659").InvalidIds();
      Assert.Equal(565656, result.First());

      result = Parser.Parse("824824821-824824827").InvalidIds();
      Assert.Equal(824824824, result.First());

      result = Parser.Parse("2121212118-2121212124").InvalidIds();
      Assert.Equal(2121212121, result.First());
    }
}
