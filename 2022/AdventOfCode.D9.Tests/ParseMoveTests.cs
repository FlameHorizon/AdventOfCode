using static AdventOfCode.D9.Program;

namespace AdventOfCode.D9.Tests
{
    public class ParseMoveTests
    {
        [Fact]
        public void ParseMoveTest()
        {
            Move moveOne = ParseMove("R 4");
            Assert.Equal(MoveDirection.Right, moveOne.Direction);
            Assert.Equal(4, moveOne.Distance);
            Assert.Equal(new Program.Vector(4, 0), moveOne.Vector);

            Move moveTwo = ParseMove("U 3");
            Assert.Equal(MoveDirection.Up, moveTwo.Direction);
            Assert.Equal(3, moveTwo.Distance);
            Assert.Equal(new Program.Vector(0, 3), moveTwo.Vector);

            Move moveThree = ParseMove("L 3");
            Assert.Equal(MoveDirection.Left, moveThree.Direction);
            Assert.Equal(3, moveThree.Distance);
            Assert.Equal(new Program.Vector(-3, 0), moveThree.Vector);

            Move moveFour = ParseMove("D 1");
            Assert.Equal(MoveDirection.Down, moveFour.Direction);
            Assert.Equal(1, moveFour.Distance);
            Assert.Equal(new Program.Vector(0, -1), moveFour.Vector) ;

        }
    }
}