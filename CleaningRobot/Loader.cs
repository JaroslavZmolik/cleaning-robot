using CleaningRobot.Model;

namespace CleaningRobot;

public static class Loader
{
    public static State LoadState(Input input)
    {
        var map = LoadMap(input.map);
        var robot = LoadRobot(input.start, input.battery);
        var commands = LoadCommands(input.commands);

        return new(map, robot, commands);
    }

    private static Map LoadMap(IEnumerable<string[]> map) =>
        new(
            map
                .Select(row => row.Select(LoadTile).ToArray())
                .ToArray()
        );

    private static Tile LoadTile(string tile) =>
        tile switch
        {
            "S" => Tile.DirtyFloor,
            "C" => Tile.Column,
            "null" => Tile.Wall,
            _ => throw new ArgumentOutOfRangeException(nameof(tile), tile, null)
        };

    private static Robot LoadRobot(Start start, int battery) =>
        new(new(start.X, start.Y), LoadRobotOrientation(start.facing), new(battery), false);

    private static Orientation LoadRobotOrientation(string startFacing) =>
        startFacing switch
        {
            "N" => Orientation.North,
            "E" => Orientation.East,
            "S" => Orientation.South,
            "W" => Orientation.West,
            _ => throw new ArgumentOutOfRangeException(nameof(startFacing), startFacing, null)
        };

    private static Commands LoadCommands(IEnumerable<string> commands) =>
        new(commands.Select(LoadCommand).ToArray());

    private static Command LoadCommand(string command) =>
        command switch
        {
            "A" => Command.Advance,
            "C" => Command.Clean,
            "TR" => Command.TurnRight,
            "TL" => Command.TurnLeft,
            "B" => Command.Back,
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
}