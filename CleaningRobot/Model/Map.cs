namespace CleaningRobot.Model;

public sealed record Map(Tile[][] Tiles)
{
    public static State Clean(State state, Position robotPosition)
    {
        if (state.Map[robotPosition] is not DirtyFloor)
        {
            return state;
        }

        state.Cleaned.Add(robotPosition);
        state.Map[robotPosition] = Tile.CleanFloor;
        return state;
    }

    public int RowsCount => Tiles[0].Length;
    public int ColumnsCount => Tiles.Length;

    public Tile this[Position position]
    {
        get => Tiles[position.Column][position.Row];
        private set => Tiles[position.Column][position.Row] = value;
    }
}

public abstract record Tile
{
    public static DirtyFloor DirtyFloor { get; } = new();
    public static CleanFloor CleanFloor { get; } = new();
    public static Column Column { get; } = new();
    public static Wall Wall { get; } = new();
}

public abstract record Floor : Tile;

public sealed record DirtyFloor : Floor;

public sealed record CleanFloor : Floor;

public sealed record Column : Tile;

public sealed record Wall : Tile;