if (args.Length != 2)
{
    Console.WriteLine("Usage: <source.json> <result.json>");
    return;
}

var inputPath = args[0];

if (!File.Exists(inputPath))
{
    Console.WriteLine($"Input file {inputPath} does not exist.");
}

var inputJson = File.ReadAllText(inputPath);
var input = Serializer.Deserialize(inputJson);
var startingState = Loader.LoadState(input);
var finalState = Runner.Start(startingState);
var output = Loader.SaveState(finalState);
var outputJson = Serializer.Serialize(output);

var outputPath = args[1];
File.WriteAllText(outputPath, outputJson);