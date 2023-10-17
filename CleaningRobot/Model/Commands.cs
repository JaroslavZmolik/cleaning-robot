namespace CleaningRobot.Model;

public sealed record Commands(Command[] Queue)
{
    private Queue<Command> _commands = new();
}

public abstract record Command
{
    public static TurnLeft TurnLeft { get; } = new();
    public static TurnRight TurnRight { get; } = new();
    public static Advance Advance { get; } = new();
    public static Back Back { get; } = new();
    public static Clean Clean { get; } = new();
}

public sealed record TurnLeft : Command;

public sealed record TurnRight : Command;

public sealed record Advance : Command;

public sealed record Back : Command;

public sealed record Clean : Command;