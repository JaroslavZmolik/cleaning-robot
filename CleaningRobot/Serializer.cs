namespace CleaningRobot;

public static class Serializer
{
    public static Input Deserialize(string input) => JsonSerializer.Deserialize<Input>(input) ?? throw new("JSON serialization failed");

    public static string Serialize(Output output) => JsonSerializer.Serialize(output);
}

public sealed record Input(string[][] map, RobotPosition start, string[] commands, int battery);

public sealed record Output(Coordinates[] visited, Coordinates[] cleaned, RobotPosition final, int battery);

public sealed record RobotPosition(int X, int Y, string facing);

public sealed record Coordinates(int X, int Y);