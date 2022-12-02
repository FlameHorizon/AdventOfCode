namespace AdventOfCode.D2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Part1();
            Part2();

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void Part1()
        {
            Console.WriteLine(
                "What would your total score be if everything "
                + "goes exactly according to your strategy guide?");

            var totalScore = GetTotalScore();
            Console.WriteLine(totalScore);
        }

        private static object GetTotalScore()
        {
            int total = 0;

            foreach (var line in File.ReadAllLines(@"data\input.txt"))
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
            else if (opponentShape == Shape.Rock && playerShape == Shape.Paper)
            {
                return GameResult.Win;
            }
            else if (opponentShape == Shape.Paper && playerShape == Shape.Scissors)
            {
                return GameResult.Win;
            } 
            else if (opponentShape == Shape.Scissors && playerShape == Shape.Rock)
            {
                return GameResult.Win;
            } 
            else
            {
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

            var totalScore = GetTotalScorePart2();
            Console.WriteLine(totalScore);
        }

        private static object GetTotalScorePart2()
        {
            int total = 0;

            foreach (var line in File.ReadAllLines(@"data\input.txt"))
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

            if (opponentShape == Shape.Paper && result == GameResult.Win)
            {
                return Shape.Scissors;
            }
            else if (opponentShape == Shape.Paper && result == GameResult.Lose)
            {
                return Shape.Rock;
            }
            else if (opponentShape == Shape.Scissors && result == GameResult.Win)
            {
                return Shape.Rock;
            }
            else if (opponentShape == Shape.Scissors && result == GameResult.Lose)
            {
                return Shape.Paper;
            }
            else if (opponentShape == Shape.Rock && result == GameResult.Win)
            {
                return Shape.Paper;
            }
            else if (opponentShape == Shape.Rock && result == GameResult.Lose)
            {
                return Shape.Scissors;
            }
            else
            {
                throw new InvalidOperationException("Can't calculate outcome of "
                    + "game given opponent shape and expected result");
            }
        }
    }
}