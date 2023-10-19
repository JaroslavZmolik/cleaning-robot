namespace CleaningRobot.Model;

public sealed record State
{
    public State(Map map, Robot robot, Commands commands)
    {
        Map = map;
        Robot = robot;
        Commands = commands;
        Visited.Add(Robot.Position);
    }

    public Map Map { get; init; }
    public Robot Robot { init; get; }
    public Commands Commands { get; init; }
    public BackOffStrategy? BackOffStrategy { get; set; }
    public HashSet<Position> Visited { get; } = new();
    public HashSet<Position> Cleaned { get; } = new();

    public bool CanExecuteNextCommand() =>
        !Robot.IsStuck
        && Commands.Count != 0
        && Commands.Queue.Peek().BatteryConsumption <= Robot.Battery.StateOfCharge;

    public State ExecuteNextCommand()
    {
        var currentCommand = Commands.Queue.Dequeue();
        var newState = currentCommand switch
        {
            TurnCommand turn => Robot.Turn(this, turn),
            MoveCommand move => Robot.Move(this, move),
            Clean => Map.Clean(this, Robot.Position),
            _ => throw new ArgumentOutOfRangeException(nameof(currentCommand))
        };
        var battery = new Battery(Robot.Battery.StateOfCharge - currentCommand.BatteryConsumption);
        return newState with { Robot = newState.Robot with { Battery = battery } };
    }

    public State InitiateBackOffSequence() =>
        BackOffStrategy switch
        {
            null => this with { BackOffStrategy = BackOffStrategy.First },
            BackOffStrategyFirst => this with { BackOffStrategy = BackOffStrategy.Second },
            BackOffStrategySecond => this with { BackOffStrategy = BackOffStrategy.Third },
            BackOffStrategyThird => this with { BackOffStrategy = BackOffStrategy.Fourth },
            BackOffStrategyFourth => this with { BackOffStrategy = BackOffStrategy.Fifth },
            BackOffStrategyFifth => this with { Robot = Robot with { IsStuck = true } },
            _ => throw new ArgumentOutOfRangeException()
        };
}