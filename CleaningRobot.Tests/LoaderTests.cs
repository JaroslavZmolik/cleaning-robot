namespace CleaningRobot.Tests;

public sealed class LoaderTests
{
    [Fact]
    public void LoadState_ShouldReturnExpectedState()
    {
        var input = TestHelper.Input1;

        var actual = Loader.LoadState(input);

        var expected = TestHelper.State1;

        actual.Map.Tiles.Should().BeEquivalentTo(expected.Map.Tiles);
        actual.Robot.Should().Be(expected.Robot);
        actual.Commands.Queue.Should().BeEquivalentTo(expected.Commands.Queue);
    }
}