namespace CleaningRobot.Model;

public sealed record Robot(Position Position, Orientation Orientation, Battery Battery, bool IsStuck)
{
    public static State Turn(State state, TurnCommand turn) =>
        state with { Robot = state.Robot with { Orientation = state.Robot.Orientation.Turn(turn) } };

    public static State Move(State state, MoveCommand move)
    {
        var moveResult = (move, state.Robot.Orientation) switch
        {
            (Advance, South) or (Back, North) => MoveOnRow(state, +1),
            (Advance, North) or (Back, South) => MoveOnRow(state, -1),
            (Advance, East) or (Back, West) => MoveOnColumn(state, +1),
            (Advance, West) or (Back, East) => MoveOnColumn(state, -1),
            _ => throw new ArgumentOutOfRangeException()
        };

        return moveResult switch
        {
            SuccessfulMove successfulMove => successfulMove.NewState,
            InvalidMove => State.InitiateBackOffSequence(state),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static MoveResult MoveOnRow(State state, int movement) =>
        MoveToNewPosition(state, state.Robot.Position with { Row = state.Robot.Position.Row + movement });

    private static MoveResult MoveOnColumn(State state, int movement) =>
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

internal abstract record MoveResult;

internal sealed record InvalidMove : MoveResult;

internal sealed record SuccessfulMove(State NewState) : MoveResult;