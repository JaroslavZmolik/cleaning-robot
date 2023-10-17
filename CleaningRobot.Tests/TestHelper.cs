namespace CleaningRobot.Tests;

public static class TestHelper
{
    public static readonly Input Input1 = new(
        new[]
        {
            new[] { "S", "S", "S", "S" },
            new[] { "S", "S", "C", "S" },
            new[] { "S", "S", "S", "S" },
            new[] { "S", "null", "S", "S" }
        },
        new(3, 0, "N"),
        new[] { "TL", "A", "C", "A", "C", "TR", "A", "C" },
        80);
}