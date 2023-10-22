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

    public int RowsCount => Tiles.Length;
    public int ColumnsCount => Tiles[0].Length;

    public Tile this[Position position]
    {
        get => Tiles[position.Row][position.Column];
        private set => Tiles[position.Row][position.Column] = value;
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