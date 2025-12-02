using static AdventOfCode.D9.Program;

namespace AdventOfCode.D9.Tests
{
    public class BoardTest
    {
        [Fact]
        public void MoveHeadTest()
        {
            var board = new Board(10);

            board.MoveHead(new Move(MoveDirection.Right, 5));
            Assert.Equal(new Program.Point(5, 0), board.PiecesPoints[0]);
            Assert.Equal(new Program.Point(4, 0), board.PiecesPoints[1]);
            Assert.Equal(new Program.Point(3, 0), board.PiecesPoints[2]);
            Assert.Equal(new Program.Point(2, 0), board.PiecesPoints[3]);
            Assert.Equal(new Program.Point(1, 0), board.PiecesPoints[4]);
            Assert.True(board.PiecesPoints.Skip(5).All(p => p.Equals(new Program.Point(0, 0))));
            Assert.Equal(1, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Up, 8));
            Assert.Equal(new Program.Point(5, 8), board.PiecesPoints[0]);
            Assert.Equal(new Program.Point(5, 7), board.PiecesPoints[1]);
            Assert.Equal(new Program.Point(5, 6), board.PiecesPoints[2]);
            Assert.Equal(new Program.Point(5, 5), board.PiecesPoints[3]);
            Assert.Equal(new Program.Point(5, 4), board.PiecesPoints[4]);
            Assert.Equal(new Program.Point(4, 4), board.PiecesPoints[5]);
            Assert.Equal(new Program.Point(3, 3), board.PiecesPoints[6]);
            Assert.Equal(new Program.Point(2, 2), board.PiecesPoints[7]);
            Assert.Equal(new Program.Point(1, 1), board.PiecesPoints[8]);
            Assert.Equal(new Program.Point(0, 0), board.PiecesPoints[9]);
            Assert.Equal(1, board.TailPositionsVisitedCount);

            board.MoveHead(new Move(MoveDirection.Left, 8));
            board.MoveHead(new Move(MoveDirection.Down, 3));
            board.MoveHead(new Move(MoveDirection.Right, 17));
            board.MoveHead(new Move(MoveDirection.Down, 10));
            board.MoveHead(new Move(MoveDirection.Left, 25));
            board.MoveHead(new Move(MoveDirection.Up, 20));

            Assert.Equal(36, board.TailPositionsVisitedCount);
        }

        [Fact]
        public void BoardInitialStateTest()
        {
            var board = new Board(2);
            Assert.Equal(new Program.Point(0, 0), board.HeadPosition);
            Assert.Equal(new Program.Point(0, 0), board.TailPosition);
        }

        [Fact]
        public void MoveHeadWithTwoPieciesTest()
        {
            var board = new Board(2);

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
    }
}
