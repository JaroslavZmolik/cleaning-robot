namespace CleaningRobot.Model;

public record Commands
{
    private Queue<Command> _commands = new();
}

public abstract record Command;

public record TurnLeft : Command;

public record TurnRight : Command;

public record Advance : Command;

public record Back : Command;

public record Clean : Command;