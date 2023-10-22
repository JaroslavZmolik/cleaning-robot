namespace CleaningRobot.Tests;

public static class TestHelper
{
    public static readonly Input Input1 = new(
        new[]
        {
            new[] { "S", "S", "S", "S" },
            new[] { "S", "S", "C", "S" },
            new[] { "S", "S", "S", "S" },
            new[] { "S", "null", "S", "S" }
        },
        new(3, 0, "N"),
        new[] { "TL", "A", "C", "A", "C", "TR", "A", "C" },
        80);

    public static readonly State State1 = new(
        new(
            new[]
            {
                new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor },
                new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.Column, Tile.DirtyFloor },
                new Tile[] { Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor, Tile.DirtyFloor },
                new Tile[] { Tile.DirtyFloor, Tile.Wall, Tile.DirtyFloor, Tile.DirtyFloor }
            }),
        new(new(3, 0), Orientation.North, new(80), false),
        new(new Command[] { Command.TurnLeft, Command.Advance, Command.Clean, Command.Advance, Command.Clean, Command.TurnRight, Command.Advance, Command.Clean }));
}