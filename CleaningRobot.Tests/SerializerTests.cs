namespace CleaningRobot.Tests;

public sealed class SerializerTests
{
    [Fact]
    public void DeserializeJson_ShouldReturnExpectedInput()
    {
        var expected = TestHelper.Input1;
        const string inputJson = """
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

        var actual = Serializer.Deserialize(inputJson);

        actual.map.Should().BeEquivalentTo(expected.map);
        actual.start.Should().Be(expected.start);
        actual.commands.Should().BeEquivalentTo(expected.commands);
        actual.battery.Should().Be(expected.battery);
    }

    [Fact]
    public void SerializeOutput_ShouldReturnExpectedJson()
    {
        const string expectedJson = """{"visited":[{"X":1,"Y":0},{"X":2,"Y":0},{"X":3,"Y":0}],"cleaned":[{"X":1,"Y":0},{"X":2,"Y":0}],"final":{"X":2,"Y":0,"facing":"N"},"battery":53}""";

        var output = new Output(
            new[]
            {
                new Coordinates(1, 0),
                new Coordinates(2, 0),
                new Coordinates(3, 0)
            },
            new[]
            {
                new Coordinates(1, 0),
                new Coordinates(2, 0)
            },
            new(2, 0, "N"),
            53);

        var actual = Serializer.Serialize(output);

        actual.Should().Be(expectedJson);
    }
}