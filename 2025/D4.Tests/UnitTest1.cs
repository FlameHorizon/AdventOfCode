using D4;
namespace D4.Tests;

public class UnitTest1
{
    [Fact]
    public void AccesableRolls_ReturnsCorrectValue()
    {
        const string initialLayout =
@"..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.";

        const int expectedRolls = 13;
        const string expectedLayoutAfterFirstIteration =
@".......@..
.@@.@.@.@@
@@@@@...@@
@.@@@@..@.
.@.@@@@.@.
.@@@@@@@.@
.@.@.@.@@@
..@@@.@@@@
.@@@@@@@@.
....@@@...";

        const string expectedLayoutAfterSecondIteration =
@"..........
.@@.....@.
.@@@@...@@
..@@@@....
.@.@@@@...
..@@@@@@..
...@.@.@@@
..@@@.@@@@
..@@@@@@@.
....@@@...";
        Assert.Equal((13, expectedLayoutAfterFirstIteration), RollsLayoutAnalyzer.AccesableRolls(initialLayout));

        // Second iteration
        Assert.Equal(12, RollsLayoutAnalyzer.AccesableRolls(expectedLayoutAfterFirstIteration).Item1);

        // Third iteration
        Assert.Equal(7, RollsLayoutAnalyzer.AccesableRolls(expectedLayoutAfterSecondIteration).Item1);
    }

    [Fact]
    public void MultiplepassAccesableRolls_ReturnsCorrectValue()
    {
        const string initialLayout =
@"..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.";

        const int expectedRolls = 43;
        Assert.Equal(expectedRolls, RollsLayoutAnalyzer.MultipassAccesableRolls(initialLayout));

    }

}
