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
    [InlineData("11-22", 2, 33)]
    [InlineData("95-115", 1, 99)]
    [InlineData("1188511880-1188511890", 1, 1188511885)]
    [InlineData("222220-222224", 1, 222222)]
    [InlineData("1698522-1698528", 0, 0)]
    public void CountsInvalidIds(string line, int count, int sum) {
      IdRange result = Parser.Parse(line);
      Assert.Equal(count, result.CountOfInvalidIds());
      Assert.Equal(sum, result.InvalidIds().Sum());
    }
}
