namespace CleaningRobot.Model;

public abstract record CommandExecutionResult;

public record CommandSuccessful(State NewState) : CommandExecutionResult;

public record ObstacleHit : CommandExecutionResult;