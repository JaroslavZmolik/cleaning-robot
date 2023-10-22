namespace CleaningRobot.Tests;

public sealed class E2ETests
{
    [Theory]
    [InlineData("../../../../TestData/test1.json", "../../../../TestData/test1_result.json")]
    [InlineData("../../../../TestData/test2.json", "../../../../TestData/test2_result.json")]
    public void WholeScenario(string inputJsonPath, string resultJsonPath)
    {
        var inputJson = File.ReadAllText(inputJsonPath);
        var input = Serializer.Deserialize(inputJson);
        var startingState = Loader.LoadState(input);
        var finalState = Runner.Start(startingState);
        var output = Loader.SaveState(finalState);
        var outputJson = Serializer.Serialize(output);
        var expectedJson = Minify(File.ReadAllText(resultJsonPath));

        outputJson.Should().Be(expectedJson);
    }

    private static string Minify(string input) => input.Replace(" ", "").Replace(Environment.NewLine, "").Replace("\n", "");
}