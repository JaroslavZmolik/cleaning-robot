using CleaningRobot.Model;

namespace CleaningRobot.Tests;

public class RunnerTests
{
    [Fact]
    public void RunCleaningProgram_ShouldReturnExpectedFinalState()
    {
        var expectedState = new State(
            new(
                new[]
                {
                    new Tile[] { Tile.DirtyFloor, Tile.CleanFloor, Tile.CleanFloor, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.Column, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.Wall, Tile.DirtyFloor, Tile.DirtyFloor }
                }),
            new(new(2, 0), Orientation.North, new(53)),
            new(Array.Empty<Command>()));
        expectedState.Visited.AddRange(new[] { new Position(1, 0), new Position(2, 0), new Position(3, 0) });
        expectedState.Cleaned.AddRange(new[] { new Position(1, 0), new Position(2, 0) });

        var actualState = Runner.RunCleaningProgram(TestHelper.State1);
    }
}