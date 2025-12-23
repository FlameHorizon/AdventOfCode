namespace D3.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData("987654321111111", 98)]
    [InlineData("811111111111119", 89)]
    [InlineData("234234234234278", 78)]
    [InlineData("818181911112111", 92)]
    public void FindMaxJoltage_ReturnsCurrentResult(string schema, int expected)
    {
        int actual = BankAnalyzer.FindMaxJoltage(schema);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("987654321111111", 987654321111)]
    [InlineData("811111111111119", 811111111119)]
    [InlineData("234234234234278", 434234234278)]
    [InlineData("818181911112111", 888911112111)]
    public void FindMaxJoltage12Batteries_ReturnsCorrectResult(string schema, long expected)
    {
        long actual = BankAnalyzer.FindMaxJoltage12Batteries(schema);
        Assert.Equal(expected, actual);
    }
}
