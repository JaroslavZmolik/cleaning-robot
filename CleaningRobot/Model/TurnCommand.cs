namespace CleaningRobot.Model;

public abstract record TurnCommand(int BatteryConsumption) : Command(BatteryConsumption)
{
    protected abstract Orientation Turn(State state);

    protected override State ExecuteProtected(State state) =>
        state with
        {
            Robot = state.Robot with
            {
                Orientation = Turn(state)
            }
        };
}

public sealed record TurnLeft() : TurnCommand(1)
{
    protected override Orientation Turn(State state) => state.Robot.Orientation.Turn(this);
}

public sealed record TurnRight() : TurnCommand(1)
{
    protected override Orientation Turn(State state) => state.Robot.Orientation.Turn(this);
}