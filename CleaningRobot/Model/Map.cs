namespace CleaningRobot.Model;

public record Map
{
    private Tile[][] _tiles = { };
}

public abstract record Tile;

public abstract record Floor : Tile;

public record DirtyFloor : Floor;

public record CleanFloor : Floor;

public record Column : Tile;

public record Wall : Tile;