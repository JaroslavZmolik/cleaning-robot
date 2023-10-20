namespace CleaningRobot.Model;

public sealed record Commands
{
    public Queue<Command> Queue { get; init; }
    public Commands(IEnumerable<Command> queue) => Queue = new(queue);
    public int Count => Queue.Count;
}

public abstract record Command(int BatteryConsumption)
{
    public static TurnLeft TurnLeft { get; } = new();
    public static TurnRight TurnRight { get; } = new();
    public static Advance Advance { get; } = new();
    public static Back Back { get; } = new();
    public static Clean Clean { get; } = new();
}

public abstract record TurnCommand(int BatteryConsumption) : Command(BatteryConsumption);

public abstract record MoveCommand(int BatteryConsumption) : Command(BatteryConsumption);

public sealed record TurnLeft() : TurnCommand(1);

public sealed record TurnRight() : TurnCommand(1);

public sealed record Advance() : MoveCommand(2);

public sealed record Back() : MoveCommand(3);

public sealed record Clean() : Command(5);