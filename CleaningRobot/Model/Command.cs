namespace CleaningRobot.Model;

public abstract record Command(int BatteryConsumption)
{
    public static TurnLeft TurnLeft { get; } = new();
    public static TurnRight TurnRight { get; } = new();
    public static Advance Advance { get; } = new();
    public static Back Back { get; } = new();
    public static Clean Clean { get; } = new();

    public State Execute(State state) => AdjustRobotBattery(ExecuteProtected(state));

    private State AdjustRobotBattery(State newState) =>
        newState with
        {
            Robot = newState.Robot with
            {
                Battery = new(newState.Robot.Battery.StateOfCharge - BatteryConsumption)
            }
        };

    protected abstract State ExecuteProtected(State state);
}