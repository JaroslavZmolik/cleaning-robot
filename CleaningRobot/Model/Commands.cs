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