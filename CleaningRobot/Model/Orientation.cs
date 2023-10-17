namespace CleaningRobot.Model;

public abstract record Orientation
{
    public static North North { get; } = new();
    public static East East { get; } = new();
    public static South South { get; } = new();
    public static West West { get; } = new();
}

public sealed record North : Orientation;

public sealed record East : Orientation;

public sealed record South : Orientation;

public sealed record West : Orientation;