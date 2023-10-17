namespace CleaningRobot.Tests;

public sealed class SerializerTests
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
        var actual = Serializer.Deserialize(input);

        var expected = TestHelper.Input1;
        actual.map.Should().BeEquivalentTo(expected.map);
        actual.start.Should().Be(expected.start);
        actual.commands.Should().BeEquivalentTo(expected.commands);
        actual.battery.Should().Be(expected.battery);
    }
}