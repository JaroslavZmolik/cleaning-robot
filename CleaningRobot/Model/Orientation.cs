namespace CleaningRobot.Model;

public abstract record Orientation
{
    public static North North { get; } = new();
    public static East East { get; } = new();
    public static South South { get; } = new();
    public static West West { get; } = new();

    public abstract Orientation Turn(TurnLeft command);
    public abstract Orientation Turn(TurnRight command);
}

public sealed record North : Orientation
{
    public override Orientation Turn(TurnLeft command) => West;

    public override Orientation Turn(TurnRight command) => East;
}

public sealed record East : Orientation
{
    public override Orientation Turn(TurnLeft command) => North;

    public override Orientation Turn(TurnRight command) => South;
}

public sealed record South : Orientation
{
    public override Orientation Turn(TurnLeft command) => East;

    public override Orientation Turn(TurnRight command) => West;
}

public sealed record West : Orientation
{
    public override Orientation Turn(TurnLeft command) => South;

    public override Orientation Turn(TurnRight command) => North;
}