namespace CleaningRobot;

public static class Runner
{
    public static State Start(State state)
    {
        var currentState = state;
        while (currentState.CanExecuteNextCommand())
        {
            if (currentState.BackOffStrategy is not null)
            {
                currentState = State.ExecuteBackOffStrategy(currentState);
                continue;
            }

            currentState = currentState.ExecuteNextCommand();
        }

        return currentState;
    }
}