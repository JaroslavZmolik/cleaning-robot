namespace CleaningRobot.Model;

public sealed record Clean() : Command(5)
{
    protected override State ExecuteProtected(State state)
    {
        var robotPosition = state.Robot.Position;
        if (state.Map[robotPosition] is not DirtyFloor)
        {
            return state;
        }

        state.Cleaned.Add(robotPosition);
        state.Map[robotPosition] = Tile.CleanFloor;
        return state;
    }
}