namespace AdventOfCode.D10.Tests
{
    public class MonitorTests
    {
        [Fact]
        public void InitMonitorTest()
        {
            var monitor = new Program.Monitor();

            (int start, int end) spriteExpected = (0, 2);
            (int x, int y) pixelExpected = (0, 0);
            Assert.Equal(spriteExpected, monitor.SpritePosition);
            Assert.Equal(pixelExpected, monitor.PixelPosition);
        }

        [Fact]
        public void MoveSpriteFrowardTest()
        {
            var monitor = new Program.Monitor();
            monitor.MoveSprite(3);

            (int start, int end) expected = (3, 5);
            Assert.Equal(expected, monitor.SpritePosition);
        }

        [Fact]
        public void SetSpritePositionTest()
        {
            var monitor = new Program.Monitor();
            monitor.SetSpritePosition(2);

            (int start, int end) expected = (1, 3);
            Assert.Equal(expected, monitor.SpritePosition);
        }

        [Fact]
        public void MoveSpriteBackwardsTest()
        {
            var monitor = new Program.Monitor();
            monitor.MoveSprite(3);
            monitor.MoveSprite(-3);

            (int start, int end) expected = (0, 2);
            Assert.Equal(expected, monitor.SpritePosition);
        }

        [Fact]
        public void MovePixelAfterCycleTest()
        {
            var monitor = new Program.Monitor();
            monitor.Tick();

            (int x, int y) pixelExpceted = (1, 0);
            Assert.Equal(pixelExpceted, monitor.PixelPosition);
        }

        [Fact]
        public void WrapPixelOnEndOfScreenTest()
        {
            var monitor = new Program.Monitor();
            for (int i = 1; i <= 40; i++)
            {
                monitor.Tick();
            }

            (int x, int y) pixelExpceted = (0, 1);
            Assert.Equal(pixelExpceted, monitor.PixelPosition);
        }

        [Fact]
        public void ShouldDrawLitPixelTest()
        {
            var monitor = new Program.Monitor();
            Assert.True(monitor.ShouldDrawLitPixel());

            monitor.Tick();
            Assert.True(monitor.ShouldDrawLitPixel());

            monitor.Tick();
            Assert.True(monitor.ShouldDrawLitPixel());

            monitor.Tick();
            Assert.False(monitor.ShouldDrawLitPixel());
        }

        [Fact]
        public void DrawLidPixelTest()
        {
            var monitor = new Program.Monitor();
            monitor.Draw();

            var expected = "#";

            Assert.Equal(expected, monitor.CurrentLine);
        }

        [Fact]
        public void DrawDarkPixelTest()
        {
            var monitor = new Program.Monitor();

            // Move out sprite range out side of current pixel to draw dark pixel.
            monitor.MoveSprite(4);
            monitor.Draw();

            var expected = ".";

            Assert.Equal(expected, monitor.CurrentLine);
        }
    }
}
