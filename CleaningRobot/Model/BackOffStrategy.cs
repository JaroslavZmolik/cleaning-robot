namespace CleaningRobot.Model;

public abstract record BackOffStrategy(Commands Commands)
{
    public static BackOffStrategyFirst First { get; } = new();
    public static BackOffStrategySecond Second { get; } = new();
    public static BackOffStrategyThird Third { get; } = new();
    public static BackOffStrategyFourth Fourth { get; } = new();
    public static BackOffStrategyFifth Fifth { get; } = new();
}

public sealed record BackOffStrategyFirst() : BackOffStrategy(new Commands(new Command[] { Command.TurnRight, Command.Advance, Command.TurnLeft }));

public sealed record BackOffStrategySecond() : BackOffStrategy(new Commands(new Command[] { Command.TurnRight, Command.Advance, Command.TurnRight }));

public sealed record BackOffStrategyThird() : BackOffStrategy(new Commands(new Command[] { Command.TurnRight, Command.Advance, Command.TurnRight }));

public sealed record BackOffStrategyFourth() : BackOffStrategy(new Commands(new Command[] { Command.TurnRight, Command.Back, Command.TurnRight, Command.Advance }));

public sealed record BackOffStrategyFifth() : BackOffStrategy(new Commands(new Command[] { Command.TurnLeft, Command.TurnLeft, Command.Advance }));