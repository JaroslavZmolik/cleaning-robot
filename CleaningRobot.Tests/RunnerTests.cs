﻿namespace CleaningRobot.Tests;

public sealed class RunnerTests
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
            new(new(2, 0), Orientation.North, new(53), false),
            new(Array.Empty<Command>()));
        expectedState.Visited.Add(new(1, 0));
        expectedState.Visited.Add(new(2, 0));
        expectedState.Visited.Add(new(3, 0));
        expectedState.Cleaned.Add(new(1, 0));
        expectedState.Cleaned.Add(new(2, 0));

        var actualState = Runner.Start(TestHelper.State1);

        actualState.Map.Tiles.Should().BeEquivalentTo(expectedState.Map.Tiles);
        actualState.BackOffStrategy.Should().BeNull();
        actualState.Cleaned.Should().BeEquivalentTo(expectedState.Cleaned);
        actualState.Visited.Should().BeEquivalentTo(expectedState.Visited);
        actualState.Robot.Should().Be(expectedState.Robot);
    }

    [Fact]
    public void RunCleaningProgram_OneTileWithMoveCommand_ShouldFinishAllBackOffStrategiesAndEndUpStuck()
    {
        var inputState = new State(
            new(
                new[]
                {
                    new Tile[] { Tile.DirtyFloor }
                }),
            new(new(0, 0), Orientation.North, new(100), false),
            new(new Command[] { Command.Advance, Command.Clean }));

        var expectedState = new State(
            new(
                new[]
                {
                    new Tile[] { Tile.DirtyFloor }
                }),
            new(new(0, 0), Orientation.South, new(81), true),
            new(Array.Empty<Command>()));

        var actualState = Runner.Start(inputState);

        actualState.Map.Tiles.Should().BeEquivalentTo(expectedState.Map.Tiles);
        actualState.Cleaned.Should().BeEquivalentTo(expectedState.Cleaned);
        actualState.Visited.Should().BeEquivalentTo(expectedState.Visited);
        actualState.Robot.Should().Be(expectedState.Robot);
    }

    [Fact]
    public void RunCleaningProgram_RobotRunOutOfBattery_ShouldFinishWithoutCompletingTheWholeSequence()
    {
        var inputState = new State(
            new(
                new[]
                {
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor },
                    new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor }
                }),
            new(new(0, 0), Orientation.East, new(27), false),
            new(new Command[] { Command.Clean, Command.Advance, Command.Clean, Command.TurnRight, Command.Advance, Command.Clean, Command.TurnRight, Command.Advance, Command.Clean }));

        var expectedState = new State(
            new(
                new[]
                {
                    new Tile[] { Tile.CleanFloor, Tile.CleanFloor },
                    new Tile[] { Tile.CleanFloor, Tile.CleanFloor }
                }),
            new(new(0, 1), Orientation.West, new(4), false),
            new(Array.Empty<Command>()));
        expectedState.Visited.Add(new(0, 0));
        expectedState.Visited.Add(new(0, 1));
        expectedState.Visited.Add(new(1, 0));
        expectedState.Visited.Add(new(1, 1));
        expectedState.Cleaned.Add(new(0, 0));
        expectedState.Cleaned.Add(new(1, 0));
        expectedState.Cleaned.Add(new(1, 1));

        var actualState = Runner.Start(inputState);

        actualState.Map.Tiles.Should().BeEquivalentTo(expectedState.Map.Tiles);
        actualState.Cleaned.Should().BeEquivalentTo(expectedState.Cleaned);
        actualState.Visited.Should().BeEquivalentTo(expectedState.Visited);
        actualState.Robot.Should().Be(expectedState.Robot);
    }
}