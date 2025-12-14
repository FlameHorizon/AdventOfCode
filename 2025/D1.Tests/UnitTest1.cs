using D1;

namespace D1.Tests;

public class UnitTest1
{
    [Fact]
    public void Dial_StartingPosition_IsValid()
    {
      var dial = new Dial();
      Assert.Equal(50, dial.StartingPosition);
    }

    [Theory]
    [InlineData("L1", -1)]
    [InlineData("R1", 1)]
    public void Dial_TurnUsingCode(string code, int amount) {
      var dial = new Dial();
      int initialPosition = dial.StartingPosition;

      dial.Turn(code);

      Assert.Equal(initialPosition + amount, dial.CurrentPosition);
    }

    [Fact]
    public void Dial_TurnRightTo99() {
      var dial = new Dial();
      dial.TurnRight(49);
      Assert.Equal(99, dial.CurrentPosition);
    }

    [Fact]
    public void Dial_TurnLeftToZero() {
      var dial = new Dial();
      dial.TurnLeft(50);
      Assert.Equal(0, dial.CurrentPosition);
    }

    [Fact]
    public void Dial_TurnRightTo0() {
      var dial = new Dial();
      dial.TurnRight(50);
      Assert.Equal(0, dial.CurrentPosition);
    }

    [Fact]
    public void Dial_TurnLeftTo99() {
      var dial = new Dial();
      dial.TurnLeft(51);
      Assert.Equal(99, dial.CurrentPosition);
    }

    [Fact]
    public void Dial_TurnRightRecords_IfLandedOnZero() {
      var dial = new Dial();
      dial.TurnRight(50);
      Assert.Equal(1, dial.PointingAtZeroCount);
    }

    [Fact]
    public void Dial_TurnLeftRecords_IfLandedOnZero() {
      var dial = new Dial();
      dial.TurnLeft(50);
      Assert.Equal(1, dial.PointingAtZeroCount);
    }

    [Fact]
    public void Dial_TurnRightRecords_IfCrossedZero() {
      var dial = new Dial();
      dial.TurnRight(51); // Points at 1.
      Assert.Equal(1, dial.CrossedZeroCount);
    }

    [Fact]
    public void Dial_TurnLeftRecords_IfCrossedZero() {
      var dial = new Dial();
      dial.TurnLeft(51); // Points at 99.
      Assert.Equal(99, dial.CurrentPosition);
      Assert.Equal(1, dial.CrossedZeroCount);
    }

    [Fact]
    public void Dial_TurnRightRecords_IfCorssedZeroManyTimes() {
      var dial = new Dial();
      dial.TurnRight(100); // Points at 50; one pass over zero
      dial.TurnRight(100); // Points at 50; one pass over zero
      dial.TurnRight(200); // Points at 50; two passes over zero
      Assert.Equal(4, dial.CrossedZeroCount);
    }

    [Fact]
    public void Dial_TurnLeftRecords_IfCorssedZeroManyTimes() {
      var dial = new Dial();
      dial.TurnLeft(100); // Points at 50; one pass over zero
      dial.TurnLeft(100); // Points at 50; one pass over zero
      dial.TurnLeft(200); // Points at 50; two passes over zero
      dial.TurnLeft(200); // Points at 50; two passes over zero
      Assert.Equal(6, dial.CrossedZeroCount);
    }

    [Fact]
    public void Dial_TurnRightRecords_IfCorssedZeroManyTimes2() {
      var dial = new Dial();
      dial.TurnRight(1000);
      Assert.Equal(0, dial.PointingAtZeroCount);
      Assert.Equal(50, dial.CurrentPosition);
      Assert.Equal(10, dial.CrossedZeroCount);
    }

    [Fact]
    public void Dail_TestCases() {
      var dial = new Dial();
      dial.TurnLeft(68);
      Assert.Equal((82, 0, 1), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(30);
      Assert.Equal((52, 0, 1), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnRight(48);
      Assert.Equal((0, 1, 1), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(5);
      Assert.Equal((95, 1, 1), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnRight(60);
      Assert.Equal((55, 1, 2), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(55);
      Assert.Equal((0, 2, 2), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(1);
      Assert.Equal((99, 2, 2), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(99);
      Assert.Equal((0, 3, 2), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnRight(14);
      Assert.Equal((14, 3, 2), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(82);
      Assert.Equal((32, 3, 3), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(100);
      Assert.Equal((32, 3, 4), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(200);
      Assert.Equal((32, 3, 6), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnRight(100);
      Assert.Equal((32, 3, 7), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnRight(200);
      Assert.Equal((32, 3, 9), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnRight(1000);
      Assert.Equal((32, 3, 19), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnLeft(1000);
      Assert.Equal((32, 3, 29), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));
    }

    [Fact]
    public void Reddit_TestCase() {
      var dial = new Dial();
      Assert.Equal(50, dial.StartingPosition);
      
      dial.TurnLeft(50);
      Assert.Equal((0, 1, 0), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));
      
      dial.TurnRight(101);
      Assert.Equal((1, 1, 1), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));
    }

    [Fact]
    public void Reddit_TestCase2() {
      var dial = new Dial();
      dial.TurnLeft(50);
      Assert.Equal((0, 1, 0), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.TurnRight(50);
      Assert.Equal((50, 1, 0), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));
    }

    [Fact]
    public void Reddit_TestCase3() {
      var dial = new Dial();
      dial.TurnLeft(250);
      Assert.Equal((0, 1, 2), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));

      dial.Reset();
      dial.TurnRight(250);
      Assert.Equal((0, 1, 2), (dial.CurrentPosition, dial.PointingAtZeroCount, dial.CrossedZeroCount));
    }
}
