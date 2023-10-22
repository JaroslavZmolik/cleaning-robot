namespace CleaningRobot.Model;

public sealed record Commands
{
    private readonly ImmutableQueue<Command> _queue;

    public Commands(IEnumerable<Command> commands)
    {
        _queue = ImmutableQueue.Create(commands.ToArray());
    }

    public bool IsEmpty => _queue.IsEmpty;

    public Command Peek() => _queue.Peek();

    public (Command item, Commands newCommands) Dequeue() => (_queue.Peek(), new(_queue.Dequeue()));
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