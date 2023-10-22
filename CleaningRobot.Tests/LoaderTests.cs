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
        actual.Commands.Should().BeEquivalentTo(expected.Commands);
    }

    [Fact]
    public void SaveState_ShouldReturnExpectedOutput()
    {
        var expectedOutput = new Output(
            new[]
            {
                new Coordinates(1, 0),
                new Coordinates(2, 0),
                new Coordinates(3, 0)
            },
            new[]
            {
                new Coordinates(1, 0),
                new Coordinates(2, 0)
            },
            new(2, 0, "N"),
            53);

        var inputState = new State(
            new(
                new[]
                {
                    new Tile[] { Tile.DirtyFloor, Tile.CleanFloor, Tile.CleanFloor, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.Column, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.Wall, Tile.DirtyFloor, Tile.DirtyFloor }
                }),
            new(new(2, 0), Orientation.North, new(53), false),
            new(Array.Empty<Command>()));
        inputState.Visited.Add(new(1, 0));
        inputState.Visited.Add(new(2, 0));
        inputState.Visited.Add(new(3, 0));
        inputState.Cleaned.Add(new(1, 0));
        inputState.Cleaned.Add(new(2, 0));

        var actual = Loader.SaveState(inputState);

        actual.visited.Should().BeEquivalentTo(expectedOutput.visited);
        actual.cleaned.Should().BeEquivalentTo(expectedOutput.cleaned);
        actual.final.Should().Be(expectedOutput.final);
        actual.battery.Should().Be(expectedOutput.battery);
    }
}