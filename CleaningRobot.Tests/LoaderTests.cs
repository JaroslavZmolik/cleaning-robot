using CleaningRobot.Model;

namespace CleaningRobot.Tests;

public sealed class LoaderTests
{
    [Fact]
    public void LoadState_ShouldReturnCorrectState()
    {
        var input = TestHelper.Input1;

        var actual = Loader.LoadState(input);

        var expected = new State(
            new(
                new[]
                {
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.Column, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.Wall, Tile.DirtyFloor, Tile.DirtyFloor }
                }),
            new(new(3, 0), new North(), new(80)),
            new(new Command[] { Command.TurnLeft, Command.Advance, Command.Clean, Command.Advance, Command.Clean, Command.TurnRight, Command.Advance, Command.Clean }));

        actual.Map.Tiles.Should().BeEquivalentTo(expected.Map.Tiles);
        actual.Robot.Should().Be(expected.Robot);
        actual.Commands.Queue.Should().BeEquivalentTo(expected.Commands.Queue);
    }
}