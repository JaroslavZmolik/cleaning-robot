namespace CleaningRobot.Model;

public record State(Tiles Map, Robot Robot, Commands Commands);

public record Tiles
{
    private Tile[][] _map;

    private Tiles(Tile[][] map)
    {
        _map = map;
    }

    public static Tiles CreateFromMap(string[][] map) => throw new NotImplementedException();
}

public abstract record Tile;

public record Floor : Tile;

public record Column : Tile;

public record Wall : Tile;

public record Robot(Position Position, Orientation Orientation, Battery Battery);

public record Battery;

public record Position;

public record Orientation;

public record Commands;