using FluentAssertions;

namespace D1.Tests;

public class MachineTests
{
    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("xtwone3four", 24)]
    public void Calibrate_Should_ReturnFirstAndLastNumber(string input, int expected)
    {
        (int First, int Last) result = Machine.Calibrate(input);
        (result.First * 10 + result.Last).Should().Be(expected);
    }
}