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
        && !Commands.IsEmpty
        && HasEnoughBatteryToExecuteCommand(Commands.Peek(), Robot.Battery);

    public State ExecuteNextCommand()
    {
        var (command, newCommands) = Commands.Dequeue();
        var newState = ExecuteCommand(this, command) with { Commands = newCommands };
        return newState;
    }

    public static State InitiateBackOffSequence(State state) =>
        state.BackOffStrategy switch
        {
            null => state with { BackOffStrategy = BackOffStrategy.First },
            BackOffStrategyFirst => state with { BackOffStrategy = BackOffStrategy.Second },
            BackOffStrategySecond => state with { BackOffStrategy = BackOffStrategy.Third },
            BackOffStrategyThird => state with { BackOffStrategy = BackOffStrategy.Fourth },
            BackOffStrategyFourth => state with { BackOffStrategy = BackOffStrategy.Fifth },
            BackOffStrategyFifth => state with { Robot = state.Robot with { IsStuck = true } },
            _ => throw new ArgumentOutOfRangeException(nameof(state.BackOffStrategy), state.BackOffStrategy, "")
        };

    public static State ExecuteBackOffStrategy(State state)
    {
        if (state.BackOffStrategy is null)
        {
            return state;
        }

        if (state.BackOffStrategy.Commands.IsEmpty)
        {
            return state with { BackOffStrategy = null };
        }

        var currentState = state;
        var (command, newCommands) = currentState.BackOffStrategy.Commands.Dequeue();
        currentState = ExecuteCommand(currentState, command) with { BackOffStrategy = currentState.BackOffStrategy with { Commands = newCommands } };

        return currentState;
    }

    private static State ExecuteCommand(State state, Command currentCommand)
    {
        var newState = currentCommand switch
        {
            TurnCommand turn => Robot.Turn(state, turn),
            MoveCommand move => Robot.Move(state, move),
            Clean => Map.Clean(state, state.Robot.Position),
            _ => throw new ArgumentOutOfRangeException(nameof(currentCommand))
        };
        var battery = new Battery(state.Robot.Battery.StateOfCharge - currentCommand.BatteryConsumption);
        return newState with { Robot = newState.Robot with { Battery = battery } };
    }

    private static bool HasEnoughBatteryToExecuteCommand(Command command, Battery battery)
    {
        return command.BatteryConsumption <= battery.StateOfCharge;
    }
}