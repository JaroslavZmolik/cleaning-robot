using CleaningRobot.Model;

namespace CleaningRobot;

public static class Runner
{
    public static State Start(State state)
    {
        var currentState = state;
        while (currentState.CanExecuteNextCommand())
        {
            currentState = currentState.ExecuteNextCommand();
        }

        return currentState;
    }
}