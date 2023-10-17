namespace CleaningRobot.Model;

public sealed record State(Map Map, Robot Robot, Commands Commands)
{
    public List<Position> Visited { get; } = new();
    public List<Position> Cleaned { get; } = new();
}