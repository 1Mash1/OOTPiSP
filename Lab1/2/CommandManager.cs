public class CommandManager
{
    private Stack<ICommand> _history = new Stack<ICommand>(); // Stores the history of executed commands for undoing

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();      // Runs the command logic
        _history.Push(command); // Saves the command to history
    }

    public void Undo()
    {
        if (_history.Count > 0) // Checks if there are any commands to revert
        {
            ICommand command = _history.Pop(); // Removes the last command from history
            command.Undo();                    // Reverts the last command's action
        }
    }
}