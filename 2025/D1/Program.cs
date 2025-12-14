using System.Diagnostics;

string[] turnCodes = File.ReadAllLines("input.txt");
var dial = new Dial();
foreach (string code in turnCodes)
{
    dial.Turn(code);
}

Console.WriteLine($"Part One.");
Debug.Assert(1043 == dial.PointingAtZeroCount, "For this input, 1043 is the only current output.");
Console.WriteLine($"Actual password is: {dial.PointingAtZeroCount}");

Console.WriteLine($"Part Two.");
Console.WriteLine($"Actual password is: {dial.PointingAtZeroCount + dial.CrossedZeroCount}");
Debug.Assert(5963 == dial.PointingAtZeroCount + dial.CrossedZeroCount, "For this input, 5963 is the only current output.");

public class Dial
{
    public readonly int StartingPosition = 50;
    public int CurrentPosition { get; private set; }
    public int PointingAtZeroCount { get; private set; }
    public int CrossedZeroCount { get; private set; }

    public Dial()
    {
        CurrentPosition = StartingPosition;
    }

    public void Turn(string code)
    {
        int amount = Convert.ToInt32(code[1..]);
        // Based on the first character of the string,
        // decide in which direcation we should spin the dial.
        if (code[0] == 'L')
        {
            TurnLeft(amount);
        }
        else
        {
            TurnRight(amount);
        }
    }

    public void TurnRight(int amount)
    {
        int endPosition = (CurrentPosition + amount) % 100;
        if (endPosition == 0) PointingAtZeroCount++;

        int fullRotationCount = amount / 100;
        if (fullRotationCount == 0 &&
            endPosition < CurrentPosition &&
            endPosition != 0)
        {
            // At this point we know, that we didn't do a full rotation
            // but we have passed zero once and right now we are behind
            // our initial position.
            CrossedZeroCount++;
        }
        else
        {
            // We did atleast one full rotation.
            CrossedZeroCount += fullRotationCount;
        }
        CurrentPosition = endPosition;
    }

    public void TurnLeft(int amount)
    {
        int endPosition = Math.Abs(CurrentPosition - amount) % 100;
        if (endPosition == 0) PointingAtZeroCount++;

        int fullRotationCount = amount / 100;

        if (fullRotationCount == 0 &&
            endPosition > CurrentPosition &&
            endPosition != 0)
        {
            CrossedZeroCount++;
        }
        else
        {
            CrossedZeroCount += fullRotationCount;
        }

        CurrentPosition = endPosition;
        return;

        int startingPosition = CurrentPosition;
        int temp = Math.Abs(CurrentPosition - amount) % 100;
        CurrentPosition = CurrentPosition - amount;

        // If we are in negative space,
        // substract neg. value from 99.
        while (CurrentPosition < 0)
        {
            int value = Math.Abs(CurrentPosition);
            CurrentPosition = 100 - value;
            CrossedZeroCount++;
        }

        if (CurrentPosition == 0)
        {
            PointingAtZeroCount++;
        }

        // skip first count if started at zero.
        if (startingPosition == 0)
        {
            CrossedZeroCount--;
        }
    }

    public void Reset()
    {
        CurrentPosition = 50;
        PointingAtZeroCount = 0;
        CrossedZeroCount = 0;
    }
}

public enum TurnDirection
{
    Left,
    Right
}
