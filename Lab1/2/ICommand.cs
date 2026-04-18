public interface ICommand
{
    void Execute(); // Performs the action and updates the state
    void Undo();    // Reverts the action and restores the state
}