namespace CleaningRobot.Model;

public abstract record Orientation;

public record North : Orientation;

public record South : Orientation;

public record West : Orientation;

public record East : Orientation;