using static AdventOfCode.D9.Program;

namespace AdventOfCode.D9.Tests
{
    public class BoardTests
    {
        [Fact]
        public void BoardInitialStateTest()
        {
            var board = new Board();

            char[,] expected = new char[,]
            {
                { '.','.','.','.','.','.' },
                { '.','.','.','.','.','.' },
                { '.','.','.','.','.','.' },
                { '.','.','.','.','.','.' },
                { 'H','.','.','.','.','.' }
            };

            Assert.Equal(expected, board.State);
            Assert.Equal(new Tuple<int, int>(0, 4), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(0, 4), board.TailPosition);
        }

        [Fact]
        public void MoveHeadTest()
        {
            var board = new Board();

            board.MoveHead(new Move(MoveDirection.Right, 4));
            Assert.Equal(new Tuple<int, int>(4, 4), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(3, 4), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Up, 4));
            Assert.Equal(new Tuple<int, int>(4, 0), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(4, 1), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Left, 3));
            Assert.Equal(new Tuple<int, int>(1, 0), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(2, 0), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Down, 1));
            Assert.Equal(new Tuple<int, int>(1, 1), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(2, 0), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Right, 4));
            Assert.Equal(new Tuple<int, int>(5, 1), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(4, 1), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Down, 1));
            Assert.Equal(new Tuple<int, int>(5, 2), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(4, 1), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Left, 5));
            Assert.Equal(new Tuple<int, int>(0, 2), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(1, 2), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Right, 2));
            Assert.Equal(new Tuple<int, int>(2, 2), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(1, 2), board.TailPosition);
        }

        [Fact]
        public void TailPositionVisitedCountTest()
        {
            var board = new Board();

            board.MoveHead(new Move(MoveDirection.Right, 4));
            Assert.Equal(new Tuple<int, int>(4, 4), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(3, 4), board.TailPosition);
            Assert.Equal(4, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Up, 4));
            Assert.Equal(new Tuple<int, int>(4, 0), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(4, 1), board.TailPosition);
            Assert.Equal(7, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Left, 3));
            Assert.Equal(new Tuple<int, int>(1, 0), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(2, 0), board.TailPosition);
            Assert.Equal(9, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Down, 1));
            Assert.Equal(new Tuple<int, int>(1, 1), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(2, 0), board.TailPosition);
            Assert.Equal(9, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Right, 4));
            Assert.Equal(new Tuple<int, int>(5, 1), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(4, 1), board.TailPosition);
            Assert.Equal(10, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Down, 1));
            Assert.Equal(new Tuple<int, int>(5, 2), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(4, 1), board.TailPosition);
            Assert.Equal(10, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Left, 5));
            Assert.Equal(new Tuple<int, int>(0, 2), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(1, 2), board.TailPosition);
            Assert.Equal(13, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Right, 2));
            Assert.Equal(new Tuple<int, int>(2, 2), board.HeadPosition);
            Assert.Equal(new Tuple<int, int>(1, 2), board.TailPosition);
            Assert.Equal(13, board.TailPositionsVisitedCount);

        }
    }
}
