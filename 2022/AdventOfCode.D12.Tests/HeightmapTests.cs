using AdventOfCode.D12;

namespace AdventOfCode.D12.Tests
{
    public class HeightmapTests
    {
        [Fact]
        public void InitHeightmapTests()
        {
            var input =
@"Saa
aaa
aaE";

            var map = new Heightmap(input);

            Assert.Equal(1, map.GetHeight(1, 0));
            Assert.Equal((0, 0), map.StartLocation);
            Assert.Equal(0, map.StartNode.Value);

            // Check whether StartNode has neighbors.
            Assert.Equal(2, map.StartNode.Children.Count);

            Assert.Equal((2, 2), map.DestinationLocation);
            Assert.Equal(27, map.DestinationNode.Value);
        }
    }
}