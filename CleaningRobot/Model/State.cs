namespace CleaningRobot.Model;

public sealed record State(Map Map, Robot Robot, Commands Commands)
{
    public Position[] Visited { get; } = Array.Empty<Position>();
}