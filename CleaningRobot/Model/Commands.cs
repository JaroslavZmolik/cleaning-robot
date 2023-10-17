namespace CleaningRobot.Model;

public sealed record Commands
{
    public Queue<Command> Queue { get; }
    public Commands(IEnumerable<Command> queue) => Queue = new(queue);
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