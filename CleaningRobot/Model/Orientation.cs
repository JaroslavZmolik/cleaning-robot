namespace CleaningRobot.Model;

public abstract record Orientation
{
    public static North North { get; } = new();
    public static East East { get; } = new();
    public static South South { get; } = new();
    public static West West { get; } = new();

    public abstract Orientation Turn(TurnCommand command);
}

public sealed record North : Orientation
{
    public override Orientation Turn(TurnCommand command) =>
        command switch
        {
            TurnLeft => West,
            TurnRight => East,
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
}

public sealed record East : Orientation
{
    public override Orientation Turn(TurnCommand command) =>
        command switch
        {
            TurnLeft => North,
            TurnRight => South,
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
}

public sealed record South : Orientation
{
    public override Orientation Turn(TurnCommand command) =>
        command switch
        {
            TurnLeft => East,
            TurnRight => West,
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
}

public sealed record West : Orientation
{
    public override Orientation Turn(TurnCommand command) =>
        command switch
        {
            TurnLeft => South,
            TurnRight => North,
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };
}