namespace CleaningRobot.Tests;

public sealed class SerializationTests
{
    [Fact]
    public void SerializeInput_ShouldGetCorrectInput()
    {
        const string input = """
                             {
                               "map": [
                                 ["S", "S", "S", "S"],
                                 ["S", "S", "C", "S"],
                                 ["S", "S", "S", "S"],
                                 ["S", "null", "S", "S"]
                               ],
                               "start": {"X": 3, "Y": 0, "facing": "N"},
                               "commands": [ "TL","A","C","A","C","TR","A","C"],
                               "battery": 80
                             }
                             """;

        var expected = new Input(
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

        var actual = InputSerializer.Load(input);

        actual.map.Should().BeEquivalentTo(expected.map);
        actual.start.Should().Be(expected.start);
        actual.commands.Should().BeEquivalentTo(expected.commands);
        actual.battery.Should().Be(expected.battery);
    }
}