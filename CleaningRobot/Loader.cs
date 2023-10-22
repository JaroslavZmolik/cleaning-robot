namespace CleaningRobot;

public static class Loader
{
    private const string NorthAbbreviation = "N";
    private const string EastAbbreviation = "E";
    private const string SouthAbbreviation = "S";
    private const string WestAbbreviation = "W";

    public static State LoadState(Input input)
    {
        var map = LoadMap(input.map);
        var robot = LoadRobot(input.start, input.battery);
        var commands = LoadCommands(input.commands);

        return new(map, robot, commands);
    }

    public static Output SaveState(State state)
    {
        var visited = SavePositions(state.Visited);
        var cleaned = SavePositions(state.Cleaned);
        var robotPosition = SaveRobot(state.Robot);

        return new(visited, cleaned, robotPosition, state.Robot.Battery.StateOfCharge);
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

    private static Robot LoadRobot(RobotPosition start, int battery) =>
        new(new(start.X, start.Y), LoadRobotOrientation(start.facing), new(battery), false);

    private static Orientation LoadRobotOrientation(string startFacing) =>
        startFacing switch
        {
            NorthAbbreviation => Orientation.North,
            EastAbbreviation => Orientation.East,
            SouthAbbreviation => Orientation.South,
            WestAbbreviation => Orientation.West,
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

    private static Coordinates[] SavePositions(HashSet<Position> positions) =>
        positions
            .OrderBy(position => position.Column)
            .ThenBy(position => position.Row)
            .Select(position => new Coordinates(position.Column, position.Row))
            .ToArray();

    private static RobotPosition SaveRobot(Robot robot) =>
        new(robot.Position.Column, robot.Position.Row, SaveRobotOrientation(robot.Orientation));

    private static string SaveRobotOrientation(Orientation orientation) =>
        orientation switch
        {
            North => NorthAbbreviation,
            East => EastAbbreviation,
            South => SouthAbbreviation,
            West => WestAbbreviation,
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
        };
}