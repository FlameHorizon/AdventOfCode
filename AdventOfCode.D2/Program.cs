// ReSharper disable SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
namespace AdventOfCode.D2;

public abstract class Program
{
    public static void Main()
    {
        Part1();
        Part2();
    }

    private static void Part1()
    {
        Console.WriteLine(
            "What would your total score be if everything "
            + "goes exactly according to your strategy guide?");

        int totalScore = GetTotalScore();
        Console.WriteLine(totalScore);
    }

    private static int GetTotalScore()
    {
        var total = 0;

        foreach (string line in File.ReadAllLines(@"data/input.txt"))
        {
            Shape opponentShape = GetOpponentShape(line);
            Shape playerShape = GetPlayerShape(line);

            // Result is calculated from the perspective of player, not the opponent.
            GameResult result = GetResult(opponentShape, playerShape); 
            int playerShapeScore = GetScoreForShape(playerShape);

            total += result switch
            {
                GameResult.Win => playerShapeScore + 6,
                GameResult.Draw => playerShapeScore + 3,
                _ => playerShapeScore,
            };
        }

        return total;
    }

    private static Shape GetOpponentShape(string line)
    {
        return line.Split(" ").First() switch 
        {
            "A" => Shape.Rock,
            "B" => Shape.Paper,
            "C" => Shape.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(line))
        };
    }

    private static Shape GetPlayerShape(string line)
    {
        return line.Split(" ")[1] switch
        {
            "X" => Shape.Rock,
            "Y" => Shape.Paper,
            "Z" => Shape.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(line))
        };
    }
        
    private static GameResult GetResult(Shape opponentShape, Shape playerShape)
    {
        if (opponentShape == playerShape)
        {
            return GameResult.Draw;
        }

        switch (opponentShape)
        {
            case Shape.Rock when playerShape == Shape.Paper:
            case Shape.Paper when playerShape == Shape.Scissors:
            case Shape.Scissors when playerShape == Shape.Rock:
                return GameResult.Win;
            default:
                return GameResult.Lose;
        }
    }

    private static int GetScoreForShape(Shape playerShape)
    {
        return playerShape switch
        {
            Shape.Rock => 1,
            Shape.Paper => 2,
            Shape.Scissors => 3,
            _ => throw new ArgumentOutOfRangeException($"This shape {playerShape} is not defined")
        };
    }

    private static void Part2()
    {
        Console.WriteLine(
            "What would your total score be if everything "
            + "goes exactly according to your strategy guide?");

        int totalScore = GetTotalScorePart2();
        Console.WriteLine(totalScore);
    }

    private static int GetTotalScorePart2()
    {
        var total = 0;

        foreach (string line in File.ReadAllLines(@"data/input.txt"))
        {
            Shape opponentShape = GetOpponentShape(line);
            GameResult result = GetExpectedResult(line);
            Shape playerShape = EvalPlayerShape(opponentShape, result);

            int playerShapeScore = GetScoreForShape(playerShape);

            total += result switch
            {
                GameResult.Win => playerShapeScore + 6,
                GameResult.Draw => playerShapeScore + 3,
                _ => playerShapeScore,
            };
        }

        return total;
    }

    private static GameResult GetExpectedResult(string line)
    {
        return line.Split(" ")[1] switch
        {
            "X" => GameResult.Lose,
            "Y" => GameResult.Draw,
            "Z" => GameResult.Win,
            _ => throw new ArgumentOutOfRangeException($"Value {line.Split(" ")[1]} outside of available range")
        };
    }

    private static Shape EvalPlayerShape(Shape opponentShape, GameResult result)
    {
        if (result == GameResult.Draw)
        {
            return opponentShape;
        }

        int moveBy = result switch
        {
            GameResult.Win => 1,
            GameResult.Lose => -1,
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };

        int index = Mod((int)opponentShape + moveBy, 3);
        return (Shape)index;
    }

    private static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}