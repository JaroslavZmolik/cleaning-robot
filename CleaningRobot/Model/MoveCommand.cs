namespace CleaningRobot.Model;

public abstract record MoveCommand(int BatteryConsumption) : Command(BatteryConsumption)
{
    protected static MoveResult MoveOnRow(State state, int movement) =>
        MoveToNewPosition(state, state.Robot.Position with { Row = state.Robot.Position.Row + movement });

    protected static MoveResult MoveOnColumn(State state, int movement) =>
        MoveToNewPosition(state, state.Robot.Position with { Column = state.Robot.Position.Column + movement });

    private static MoveResult MoveToNewPosition(State state, Position newPosition)
    {
        if (IsPositionWallOrColumn(newPosition, state.Map))
        {
            return new InvalidMove();
        }

        state.Visited.Add(newPosition);
        return new SuccessfulMove(state with { Robot = state.Robot with { Position = newPosition } });
    }

    private static bool IsPositionWallOrColumn(Position position, Map map) =>
        position.Row < 0
        || position.Row >= map.RowsCount
        || position.Column < 0
        || position.Column >= map.ColumnsCount
        || map[position] is Column;
}

public sealed record Advance() : MoveCommand(2)
{
    protected override State ExecuteProtected(State state)
    {
        var moveResult = state.Robot.Orientation switch
        {
            South => MoveOnRow(state, +1),
            North => MoveOnRow(state, -1),
            East => MoveOnColumn(state, +1),
            West => MoveOnColumn(state, -1),
            _ => throw new ArgumentOutOfRangeException()
        };

        return moveResult switch
        {
            SuccessfulMove successfulMove => successfulMove.NewState,
            InvalidMove => State.InitiateBackOffSequence(state),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public sealed record Back() : MoveCommand(3)
{
    protected override State ExecuteProtected(State state)
    {
        var moveResult = state.Robot.Orientation switch
        {
            South => MoveOnRow(state, -1),
            North => MoveOnRow(state, +1),
            East => MoveOnColumn(state, -1),
            West => MoveOnColumn(state, +1),
            _ => throw new ArgumentOutOfRangeException()
        };

        return moveResult switch
        {
            SuccessfulMove successfulMove => successfulMove.NewState,
            InvalidMove => State.InitiateBackOffSequence(state),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public abstract record MoveResult;

public sealed record InvalidMove : MoveResult;

public sealed record SuccessfulMove(State NewState) : MoveResult;