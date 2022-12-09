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
            Assert.Equal(new Program.Point(0, 0), board.HeadPosition);
            Assert.Equal(new Program.Point(0, 0), board.TailPosition);
        }

        [Fact]
        public void MoveHeadTest()
        {
            var board = new Board();

            board.MoveHead(new Move(MoveDirection.Right, 4));
            Assert.Equal(new Program.Point(4, 0), board.HeadPosition);
            Assert.Equal(new Program.Point(3, 0), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Up, 4));
            Assert.Equal(new Program.Point(4, 4), board.HeadPosition);
            Assert.Equal(new Program.Point(4, 3), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Left, 3));
            Assert.Equal(new Program.Point(1, 4), board.HeadPosition);
            Assert.Equal(new Program.Point(2, 4), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Down, 1));
            Assert.Equal(new Program.Point(1, 3), board.HeadPosition);
            Assert.Equal(new Program.Point(2, 4), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Right, 4));
            Assert.Equal(new Program.Point(5, 3), board.HeadPosition);
            Assert.Equal(new Program.Point(4, 3), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Down, 1));
            Assert.Equal(new Program.Point(5, 2), board.HeadPosition);
            Assert.Equal(new Program.Point(4, 3), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Left, 5));
            Assert.Equal(new Program.Point(0, 2), board.HeadPosition);
            Assert.Equal(new Program.Point(1, 2), board.TailPosition);

            board.MoveHead(new Move(MoveDirection.Right, 2));
            Assert.Equal(new Program.Point(2, 2), board.HeadPosition);
            Assert.Equal(new Program.Point(1, 2), board.TailPosition);
        }

        [Fact]
        public void TailPositionVisitedCountTest()
        {
            var board = new Board();

            board.MoveHead(new Move(MoveDirection.Right, 4));
            Assert.Equal(4, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Up, 4));
            Assert.Equal(7, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Left, 3));
            Assert.Equal(9, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Down, 1));
            Assert.Equal(9, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Right, 4));
            Assert.Equal(10, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Down, 1));
            Assert.Equal(10, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Left, 5));
            Assert.Equal(13, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Right, 2));
            Assert.Equal(13, board.TailPositionsVisitedCount);

        }
    }
}
