using System.Text.Json;

namespace CleaningRobot.Serialization;

public static class InputSerializer
{
    public static Input Load(string input) => JsonSerializer.Deserialize<Input>(input) ?? throw new("JSON serialization failed");
}

public sealed record Input(string[][] map, Start start, string[] commands, int battery);

public sealed record Start(int X, int Y, string facing);